import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProdutoTipoService } from '../../core/services/produto-tipo.service';
import { MatDialog } from '@angular/material/dialog';
import { ProdutoTipoModalComponent } from './produto-tipo-modal/produto-tipo-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

export interface ProdutoTipo {
    idProdutoTipo: number;
    nmProdutoTipo: string;
}

@Component({
    selector: 'app-produto-tipo',
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule],
    templateUrl: './produto-tipo.component.html'
})

export class ProdutoTipoComponent implements OnInit {
    private readonly produtoTipoService = inject(ProdutoTipoService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<ProdutoTipo>([]);
    displayedColumns: string[] = ['nmProdutoTipo', 'acoes'];

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarProdutoTipoes();
    }

    carregarProdutoTipoes(): void {
        this.produtoTipoService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
                this.dataSource.paginator = this.paginacao;
                this.dataSource.sort = this.ordenacao;
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar tipos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar tipos: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(produtoTipo?: ProdutoTipo): void {
        const dialogRef = this.dialog.open(ProdutoTipoModalComponent, { width: '500px', data: produtoTipo ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idProdutoTipo && resultado.idProdutoTipo > 0) {
                    this.produtoTipoService.atualizar(resultado).subscribe({
                        next: () => this.carregarProdutoTipoes(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro);
                        }
                    });
                } else {
                    this.produtoTipoService.cadastrar(resultado).subscribe({
                        next: () => this.carregarProdutoTipoes(),
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
                this.produtoTipoService.excluir(id).subscribe({
                    next: () => this.carregarProdutoTipoes(),
                    error: (erro) => {
                        this.snackBar.open(`Não excluído: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                        console.error('Não excluído: ', erro);
                    }
                });
            }
        });
    }
}