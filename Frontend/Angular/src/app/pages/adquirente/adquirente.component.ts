import { Component, OnInit, ViewChild, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AdquirenteService } from '../../core/services/adquirente.service';
import { MatDialog } from '@angular/material/dialog';
import { AdquirenteModalComponent } from './adquirente-modal/adquirente-modal.component';
import { DialogoConfirmacaoComponent } from '../../shared/components/dialogo-confirmacao.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';

export interface Adquirente {
    idAdquirente: number;
    nmAdquirente: string;
    txEndereco: string;
    txAnotacao: string;
}

@Component({
    selector: 'app-adquirente',
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatInputModule, MatFormFieldModule, MatSnackBarModule, MatProgressBarModule],
    templateUrl: './adquirente.component.html'
})

export class AdquirenteComponent implements OnInit {
    private readonly adquirenteService = inject(AdquirenteService);
    private readonly dialog = inject(MatDialog);
    private readonly snackBar = inject(MatSnackBar);
    dataSource = new MatTableDataSource<Adquirente>([]);
    displayedColumns: string[] = ['nmAdquirente', 'txEndereco', 'txAnotacao', 'acoes'];
    abrindo = signal<boolean>(false);

    @ViewChild(MatPaginator) paginacao!: MatPaginator;
    @ViewChild(MatSort) ordenacao!: MatSort;

    ngOnInit(): void {
        this.carregarAdquirentes();
    }

    carregarAdquirentes(): void {
        this.abrindo.set(true);
        this.adquirenteService.listar().subscribe({
            next: (dados) => {
                this.dataSource.data = dados;
                this.dataSource.paginator = this.paginacao;
                this.dataSource.sort = this.ordenacao;
                this.abrindo.set(false);
            },
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar adquirentes: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                this.abrindo.set(false);
                console.error('Erro ao carregar adquirentes: ', erro);
            }
        });
    }

    aplicarFiltro(event: Event): void {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();    
        if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
    }

    abrirModal(adquirente?: Adquirente): void {
        const dialogRef = this.dialog.open(AdquirenteModalComponent, { width: '500px', data: adquirente ?? null });
        dialogRef.afterClosed().subscribe(resultado => {
            if (resultado) {
                if (resultado.idAdquirente && resultado.idAdquirente > 0) {
                    this.adquirenteService.atualizar(resultado).subscribe({
                        next: () => this.carregarAdquirentes(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                            console.error('Não atualizado: ', erro);
                        }
                    });
                } else {
                    this.adquirenteService.cadastrar(resultado).subscribe({
                        next: () => this.carregarAdquirentes(),
                        error: (erro) => {
                            this.snackBar.open(`Não atualizado: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
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
                this.adquirenteService.excluir(id).subscribe({
                    next: () => {
                        this.carregarAdquirentes(),
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