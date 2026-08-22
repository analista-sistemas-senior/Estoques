import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProdutoListaResposta } from '../../../core/services/produto.service';
import { ProdutoFabricanteService } from '../../../core/services/produto-fabricante.service';
import { ProdutoSituacaoService } from '../../../core/services/produto-situacao.service';
import { ProdutoTipoService } from '../../../core/services/produto-tipo.service';
import { ProdutoMedidaService } from '../../../core/services/produto-medida.service';
import { MatSelectModule } from '@angular/material/select';
import { ProdutoCor } from '../../../shared/enums/produto-cor.enum';
import { MatTabsModule } from '@angular/material/tabs';
import { environment } from '../../../../environments/environment';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
    selector: 'app-produto-dialog',
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatSelectModule, MatTabsModule, MatSnackBarModule],
    templateUrl: './produto-modal.component.html'
})

export class ProdutoModalComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly dialogRef = inject(MatDialogRef<ProdutoModalComponent>);
    private readonly snackBar = inject(MatSnackBar);
    private readonly produtoFabricanteService = inject(ProdutoFabricanteService);
    private readonly produtoSituacaoService = inject(ProdutoSituacaoService);
    private readonly produtoTipoService = inject(ProdutoTipoService);
    private readonly produtoMedidaService = inject(ProdutoMedidaService);
    private readonly cdr = inject(ChangeDetectorRef);
    data: ProdutoListaResposta | null = inject(MAT_DIALOG_DATA);
    apiBase = environment.apiUrlBase;
    editando = !!this.data;
    produtoTipo: any[] = [];
    produtoSituacao: any[] = [];
    produtoFabricante: any[] = [];
    produtoMedida: any[] = [];
    produtoImagem: string | ArrayBuffer | null = null;
    produtoImagemSelecionada: File | null = null;
    produtoCores = Object.keys(ProdutoCor).filter((key) => isNaN(Number(key))).map((key) => ({ id: ProdutoCor[key as keyof typeof ProdutoCor], nome: key })).sort((a, b) => a.nome.localeCompare(b.nome));

    formulario: FormGroup = this.fb.group({
        idProduto: [this.data?.idProduto ?? 0],
        idProdutoTipo: [this.data?.idProdutoTipo, [Validators.required]],
        idProdutoSituacao: [this.data?.idProdutoSituacao, [Validators.required]],
        idProdutoFabricante: [this.data?.idProdutoFabricante, [Validators.required]],
        idProdutoMedida: [this.data?.idProdutoMedida],
        nmProduto: [this.data?.nmProduto, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
        dsProduto: [this.data?.dsProduto, [Validators.minLength(3), Validators.maxLength(1024)]],
        inProdutoCor: [this.data?.inProdutoCor, [Validators.required]],
        lkProdutoImagem: [this.data?.lkProdutoImagem, [Validators.minLength(3), Validators.maxLength(1024)]],
        txAnotacao: [this.data?.txAnotacao, [Validators.minLength(3), Validators.maxLength(255)]]
    });

    ngOnInit(): void {
        this.carregarSelects();
        if (this.data) this.produtoImagem = this.data.lkProdutoImagem;
    }

    carregarSelects(): void {
        this.produtoFabricanteService.listar().subscribe({
            next: (dados) => (this.produtoFabricante = dados),
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar tipos de produtos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar tipos de produtos: ', erro)
            }
        });
    
        this.produtoSituacaoService.listar().subscribe({
          next: (dados) => (this.produtoSituacao = dados),
          error: (erro) => {
                this.snackBar.open(`Erro ao carregar situações dos produtos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar situações dos produtos: ', erro)
          }
        });

        this.produtoTipoService.listar().subscribe({
            next: (dados) => (this.produtoTipo = dados),
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar tipos dos produtos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar tipos dos produtos: ', erro)
            }
        });

        this.produtoMedidaService.listar().subscribe({
            next: (dados) => (this.produtoMedida = dados),
            error: (erro) => {
                this.snackBar.open(`Erro ao carregar medidas dos produtos: ${erro}`, 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                console.error('Erro ao carregar medidas dos produtos: ', erro)
            }
        });
    }

    salvar(): void {
        if (this.formulario.valid) {
            const dadosEnvio = { ...this.formulario.value, arquivo: this.produtoImagemSelecionada };
            this.dialogRef.close(dadosEnvio);
        }
    }

    fechar(): void {
        this.dialogRef.close();
    }

    onFileSelected(event: Event): void {
        const input = event.target as HTMLInputElement;

        if (input.files && input.files[0]) {
            this.produtoImagemSelecionada = input.files[0];        
            const reader = new FileReader();
            reader.onload = () => {
                this.produtoImagem = reader.result as string;
                this.cdr.detectChanges();
            };
            reader.readAsDataURL(this.produtoImagemSelecionada);
        }
    }

    get imagemVista(): string {
        if (typeof this.produtoImagem === 'string' && this.produtoImagem.startsWith('data:')) {
            return this.produtoImagem;
        }
        return `${this.apiBase}${this.produtoImagem}`;
    }
}