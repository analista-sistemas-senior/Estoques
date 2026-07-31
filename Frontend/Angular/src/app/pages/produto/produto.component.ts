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
import { MatDialog } from '@angular/material/dialog';
import { ProdutoModalComponent } from './produto-modal/produto-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { environment } from '../../../environments/environment';
import { ProdutoCor } from '../../shared/enums/produto-cor.enum';
import { ProdutoMedida } from '../../shared/enums/produto-medida.enum';
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
}

@Component({
    selector: 'app-produto',
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule],
    templateUrl: './produto.component.html'
})

export class ProdutoComponent implements OnInit {
    private readonly produtoService = inject(ProdutoService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<Produto>([]);
    displayedColumns: string[] = ['lkProdutoImagem', 'nmProduto', 'dsProduto', 'produtoTipo', 'produtoFabricante', 'inProdutoCor', 'inProdutoMedida', 'acoes'];
    apiBase = environment.apiUrlBase;

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

    abrirModal(produto?: Produto): void {
        const dialogRef = this.dialog.open(ProdutoModalComponent, { width: '500px', data: produto ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idProduto && resultado.idProduto > 0) {
                    this.produtoService.atualizar(resultado).subscribe({
                        next: () => this.carregarProdutos(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro);
                        }
                    });
                } else {
                    this.produtoService.cadastrar(resultado).subscribe({
                        next: () => this.carregarProdutos(),
                        error: (erro) => {
                            this.snackBar.open(`Não cadastrado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não cadastrado: ', erro);
                        }
                    });
                }
            }
        });
    }

    excluir(id: number): void {
        const dialogRef = this.dialog.open(DialogoConfirmacaoComponent, {
          width: '400px', data: {}
        });
        dialogRef.afterClosed().subscribe((confirmado: boolean) => {
            if (confirmado) {
                this.produtoService.excluir(id).subscribe({
                    next: () => this.carregarProdutos(),
                    error: (erro) => {
                        this.snackBar.open(`Não excluído: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                        console.error('Não excluído: ', erro);
                    }
                });
            }
        });
    }

    retornarCor(id: number): string {
        return ProdutoCor[id];
    }

    retornarMedida(id: number): string {
        return ProdutoMedida[id] ?? '-';
    }
}