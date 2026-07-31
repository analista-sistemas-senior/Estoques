import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FornecedorService } from '../../core/services/fornecedor.service';
import { MatDialog } from '@angular/material/dialog';
import { FornecedorModalComponent } from './fornecedor-modal/fornecedor-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

export interface Fornecedor {
    idFornecedor: number;
    nmFornecedor: string;
    txEndereco: string;
    txAnotacao: string;
}

@Component({
    selector: 'app-fornecedores',
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule],
    templateUrl: './fornecedor.component.html'
})

export class FornecedorComponent implements OnInit {
    private readonly fornecedorService = inject(FornecedorService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<Fornecedor>([]);
    displayedColumns: string[] = ['nmFornecedor', 'txEndereco', 'txAnotacao', 'acoes'];

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarFornecedores();
    }

    carregarFornecedores(): void {
        this.fornecedorService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
                this.dataSource.paginator = this.paginacao;
                this.dataSource.sort = this.ordenacao;
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar fornecedores: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar fornecedores: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(fornecedor?: Fornecedor): void {
        const dialogRef = this.dialog.open(FornecedorModalComponent, { width: '500px', data: fornecedor ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idFornecedor && resultado.idFornecedor > 0) {
                    this.fornecedorService.atualizar(resultado).subscribe({
                        next: () => this.carregarFornecedores(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro)
                        }
                    });
                } else {
                    this.fornecedorService.cadastrar(resultado).subscribe({
                        next: () => this.carregarFornecedores(),
                        error: (erro) => {
                            this.snackBar.open(`Não cadastrado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não cadastrado: ', erro)
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
                this.fornecedorService.excluir(id).subscribe({
                    next: () => this.carregarFornecedores(),
                    error: (erro) => {
                        this.snackBar.open(`Não excluído: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                        console.error('Não excluído: ', erro);
                    }
                });
            }
        });
    }
}