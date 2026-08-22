import { Component, OnInit, ViewChild, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProdutoMedidaService } from '../../core/services/produto-medida.service';
import { MatDialog } from '@angular/material/dialog';
import { ProdutoMedidaModalComponent } from './produto-medida-modal/produto-medida-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';

export interface ProdutoMedida {
    idProdutoMedida: number;
    mdProdutoMedida: string;
}

@Component({
  selector: 'app-produto-medida',
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule, MatProgressBarModule],
  templateUrl: './produto-medida.component.html'
})

export class ProdutoMedidaComponent implements OnInit {
    private readonly produtoMedidaService = inject(ProdutoMedidaService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<ProdutoMedida>([]);
    displayedColumns: string[] = ['mdProdutoMedida', 'acoes'];
    abrindo = signal<boolean>(false);

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarProdutoMedidas();
    }

    carregarProdutoMedidas(): void {
        this.abrindo.set(true);
        this.produtoMedidaService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
                this.dataSource.paginator = this.paginacao;
                this.dataSource.sort = this.ordenacao;
                this.abrindo.set(false);
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar medidas: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                this.abrindo.set(false);
                console.error('Erro ao carregar medidas: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(produtoMedida?: ProdutoMedida): void {
        const dialogRef = this.dialog.open(ProdutoMedidaModalComponent, { width: '500px', data: produtoMedida ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idProdutoMedida && resultado.idProdutoMedida > 0) {
                    this.produtoMedidaService.atualizar(resultado).subscribe({
                        next: () => this.carregarProdutoMedidas(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro)
                        }
                    });
                } else {
                    this.produtoMedidaService.cadastrar(resultado).subscribe({
                        next: () => this.carregarProdutoMedidas(),
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
                this.abrindo.set(true);
                this.produtoMedidaService.excluir(id).subscribe({
                    next: () => {
                        this.carregarProdutoMedidas(),
                        this.abrindo.set(false);
                    },
                    error: (erro) => {
                        this.snackBar.open(`Não excluído: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                        console.error('Não excluído: ', erro);
                    }
                });
            }
        });
    }
}