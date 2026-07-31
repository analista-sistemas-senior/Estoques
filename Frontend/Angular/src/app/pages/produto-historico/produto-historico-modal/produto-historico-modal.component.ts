import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, formatDate } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProdutoHistoricoListaResposta } from '../../../core/services/produto-historico.service';
import { FornecedorService } from '../../../core/services/fornecedor.service';
import { AdquirenteService } from '../../../core/services/adquirente.service';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter, MAT_DATE_LOCALE } from '@angular/material/core';
import { ProdutoHistorico } from '../../../shared/enums/produto-historico.enum';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

export interface ProdutoHistoricoModalData {
    idProduto: number;
    nmProduto: string;
    produtoHistorico?: ProdutoHistoricoListaResposta | null; 
}

@Component({
    selector: 'app-produto-historico-dialog',
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatSelectModule, MatDatepickerModule, MatSnackBarModule],
    providers: [ provideNativeDateAdapter(), { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' } ],
    templateUrl: './produto-historico-modal.component.html'
})

export class ProdutoHistoricoModalComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly fornecedorService = inject(FornecedorService);
    private readonly adquirenteService = inject(AdquirenteService);
    private readonly dialogRef = inject(MatDialogRef<ProdutoHistoricoModalComponent>);
    private readonly snackBar = inject(MatSnackBar);
    data: ProdutoHistoricoModalData | null = inject(MAT_DIALOG_DATA);
    editando = !!this.data;
    nomeProduto = this.data?.nmProduto;
    fornecedores: any[] = [];
    adquirentes: any[] = [];
    tipoHistorico = Object.keys(ProdutoHistorico).filter((key) => isNaN(Number(key))).map((key) => ({ id: ProdutoHistorico[key as keyof typeof ProdutoHistorico], nome: key }));

    formulario: FormGroup = this.fb.group({
        idProdutoHistorico: [this.data?.produtoHistorico?.idProdutoHistorico ?? 0],
        idProduto: [this.data?.idProduto, [Validators.required]],
        idFornecedor: [this.data?.produtoHistorico?.idFornecedor, [Validators.required]],
        idAdquirente: [this.data?.produtoHistorico?.idAdquirente], 
        inProdutoHistoricoTipo: [this.data?.produtoHistorico?.inProdutoHistoricoTipo, [Validators.required]],
        dtProdutoHistorico: [this.data?.produtoHistorico?.dtProdutoHistorico, [Validators.required]],
        qtProdutoHistorico: [this.data?.produtoHistorico?.qtProdutoHistorico, [Validators.required, Validators.min(0.0001), Validators.max(Number.MAX_SAFE_INTEGER)]],
        vlProdutoHistorico: [this.data?.produtoHistorico?.vlProdutoHistorico, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_SAFE_INTEGER)]]
    });

    ngOnInit(): void {
        this.carregarSelects();
    }

    carregarSelects(): void {
        this.fornecedorService.listar().subscribe({
            next: (dados) => (this.fornecedores = dados),
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar fornecedores: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar fornecedores: ', erro);
            }
        });
    
        this.adquirenteService.listar().subscribe({
            next: (dados) => (this.adquirentes = dados),
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar adquirentes: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar adquirentes: ', erro);
            }
        });
    }

    salvar(): void {
        if (this.formulario.valid) {
            const dados = { ...this.formulario.value };
            if (dados.dtProdutoHistorico instanceof Date) {
                dados.dtProdutoHistorico = formatDate(dados.dtProdutoHistorico, 'yyyy-MM-dd', 'pt-BR');
            }
            this.dialogRef.close(dados);
        }
    }

    fechar(): void {
        this.dialogRef.close();
    }
}