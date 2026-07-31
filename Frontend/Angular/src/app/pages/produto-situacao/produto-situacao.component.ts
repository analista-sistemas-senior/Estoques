import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProdutoSituacaoService } from '../../core/services/produto-situacao.service';
import { MatDialog } from '@angular/material/dialog';
import { ProdutoSituacaoModalComponent } from './produto-situacao-modal/produto-situacao-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

export interface ProdutoSituacao {
    idProdutoSituacao: number;
    nmProdutoSituacao: string;
}

@Component({
    selector: 'app-produto-situacao',
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule],
    templateUrl: './produto-situacao.component.html'
})

export class ProdutoSituacaoComponent implements OnInit {
    private readonly produtoSituacaoService = inject(ProdutoSituacaoService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<ProdutoSituacao>([]);
    displayedColumns: string[] = ['nmProdutoSituacao', 'acoes'];

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarProdutoSituacaoes();
    }

    carregarProdutoSituacaoes(): void {
        this.produtoSituacaoService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
                this.dataSource.paginator = this.paginacao;
                this.dataSource.sort = this.ordenacao;
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar situacaos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar situacaos: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(produtoSituacao?: ProdutoSituacao): void {
        const dialogRef = this.dialog.open(ProdutoSituacaoModalComponent, { width: '500px', data: produtoSituacao ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idProdutoSituacao && resultado.idProdutoSituacao > 0) {
                    this.produtoSituacaoService.atualizar(resultado).subscribe({
                        next: () => this.carregarProdutoSituacaoes(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro);
                        }
                    });
                } else {
                    this.produtoSituacaoService.cadastrar(resultado).subscribe({
                        next: () => this.carregarProdutoSituacaoes(),
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
                this.produtoSituacaoService.excluir(id).subscribe({
                    next: () => this.carregarProdutoSituacaoes(),
                    error: (erro) => {
                        this.snackBar.open(`Não excluído: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                        console.error('Não excluído: ', erro);
                    }
                });
            }
        });
    }
}