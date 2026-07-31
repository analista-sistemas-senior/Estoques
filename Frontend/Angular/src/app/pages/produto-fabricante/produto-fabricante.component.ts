import { Component, OnInit, ViewChild, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProdutoFabricanteService } from '../../core/services/produto-fabricante.service';
import { MatDialog } from '@angular/material/dialog';
import { ProdutoFabricanteModalComponent } from './produto-fabricante-modal/produto-fabricante-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';

export interface ProdutoFabricante {
    idProdutoFabricante: number;
    nmProdutoFabricante: string;
}

@Component({
  selector: 'app-produto-fabricante',
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule, MatProgressBarModule],
  templateUrl: './produto-fabricante.component.html'
})

export class ProdutoFabricanteComponent implements OnInit {
    private readonly produtoFabricanteService = inject(ProdutoFabricanteService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<ProdutoFabricante>([]);
    displayedColumns: string[] = ['nmProdutoFabricante', 'acoes'];
    abrindo = signal<boolean>(false);

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarProdutoFabricantes();
    }

    carregarProdutoFabricantes(): void {
        this.abrindo.set(true);
        this.produtoFabricanteService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
                this.dataSource.paginator = this.paginacao;
                this.dataSource.sort = this.ordenacao;
                this.abrindo.set(false);
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar fabricantes: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                this.abrindo.set(false);
                console.error('Erro ao carregar fabricantes: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(produtoFabricante?: ProdutoFabricante): void {
        const dialogRef = this.dialog.open(ProdutoFabricanteModalComponent, { width: '500px', data: produtoFabricante ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idProdutoFabricante && resultado.idProdutoFabricante > 0) {
                    this.produtoFabricanteService.atualizar(resultado).subscribe({
                        next: () => this.carregarProdutoFabricantes(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro)
                        }
                    });
                } else {
                    this.produtoFabricanteService.cadastrar(resultado).subscribe({
                        next: () => this.carregarProdutoFabricantes(),
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
                this.produtoFabricanteService.excluir(id).subscribe({
                    next: () => {
                        this.carregarProdutoFabricantes(),
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