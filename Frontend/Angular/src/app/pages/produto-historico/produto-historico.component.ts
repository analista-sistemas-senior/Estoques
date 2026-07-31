import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProdutoService } from '../../core/services/produto.service';
import { ProdutoHistoricoService } from '../../core/services/produto-historico.service';
import { MatDialog } from '@angular/material/dialog';
import { ProdutoHistoricoModalComponent } from './produto-historico-modal/produto-historico-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { environment } from '../../../environments/environment';
import { ProdutoCor } from '../../shared/enums/produto-cor.enum';
import { ProdutoMedida } from '../../shared/enums/produto-medida.enum';
import { ProdutoHistorico } from '../../shared/enums/produto-historico.enum';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

export interface Produto {
    idProduto: number;
    idProdutoTipo: number;
    idProdutoSituacao: number;
    idProdutoFabricante: number;
    nmProduto: string;
    dsProduto: string;
    inProdutoCor: number;
    inProdutoMedida: number;
    lkProdutoImagem: string;
    produtoHistorico?: ProdutoHistoricoItem[];
}
export interface ProdutoHistoricoItem {
    idProdutoHistorico: number;
    idProduto: number;
    idFornecedor: number;
    idAdquirente: number;
    inProdutoHistoricoTipo: number;
    dtProdutoHistorico: Date;
    qtProdutoHistorico: number;
    vlProdutoHistorico: number;
}

@Component({
    selector: 'app-produto-historico',
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule],
    templateUrl: './produto-historico.component.html',
    styleUrl: './produto-historico.component.css'
})

export class ProdutoHistoricoComponent implements OnInit {
    private readonly produtoService = inject(ProdutoService);
    private readonly produtoHistoricoService = inject(ProdutoHistoricoService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<Produto>([]);
    displayedColumns: string[] = ['expandir','lkProdutoImagem', 'nmProduto', 'dsProduto', 'produtoTipo', 'produtoFabricante', 'inProdutoCor', 'qtProduto', 'inProdutoMedida', 'acoes'];
    apiBase = environment.apiUrlBase;
    elementoExpandido: number | null = null;
    tipoHistorico = Object.keys(ProdutoHistorico).filter((key) => isNaN(Number(key))).map((key) => ({ id: ProdutoHistorico[key as keyof typeof ProdutoHistorico], nome: key }));

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarProdutos();
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginacao;
        this.dataSource.sort = this.ordenacao;
    }

    carregarProdutos(): void {
        this.produtoService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar produtos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar produtos: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(produto: Produto, produtoHistorico?: ProdutoHistorico): void {
        const dialogRef = this.dialog.open(ProdutoHistoricoModalComponent, { width: '500px', data: { 
            idProduto: produto.idProduto,
            nmProduto: produto.nmProduto,
            produtoHistorico: produtoHistorico ?? null
        }});
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idProdutoHistorico && resultado.idProdutoHistorico > 0) {
                    this.produtoHistoricoService.atualizar(resultado).subscribe({
                        next: () => this.carregarProdutos(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro)
                        }
                    });
                } else {
                    this.produtoHistoricoService.cadastrar(resultado).subscribe({
                        next: () => this.carregarProdutos(),
                        error: (erro) => {
                            this.snackBar.open(`Não cadastrado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não cadastrado: ', erro)
                        }
                    });
                }
                this.elementoExpandido = null;
            }
        });
    }

    excluir(id: number): void {
        const dialogRef = this.dialog.open(DialogoConfirmacaoComponent, {
          width: '400px', data: {}
        });
        dialogRef.afterClosed().subscribe((confirmado: boolean) => {
            if (confirmado) {
                this.produtoHistoricoService.excluir(id).subscribe({
                    next: () => this.carregarProdutos(),
                    error: (erro) => {
                        this.snackBar.open(`Não excluído: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                        console.error('Não excluído: ', erro)
                    }
                });
            }
        });
    }

    retornarCor(id: number): string {
        return ProdutoCor[id];
    }

    retornarMedida(id: number): string {
        return ProdutoMedida[id] ?? 'Não informada';
    }

    toggleRow(elemento: Produto) {
        if (this.elementoExpandido == elemento.idProduto) {
            this.elementoExpandido = null;
            return;
        }

        this.produtoHistoricoService.listarPorProduto(elemento.idProduto).subscribe({
            next: (dadosHistorico) => {
                elemento.produtoHistorico = dadosHistorico;
                this.elementoExpandido = elemento.idProduto;
                this.dataSource.data = [...this.dataSource.data];
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao buscar histórico: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao buscar histórico', erro);
            }
        });
    }

    retornaNomeTipoHistorico(tipoId: number): string {
        const item = this.tipoHistorico.find(t => t.id == tipoId);
        return item ? item.nome : '';
    }
}