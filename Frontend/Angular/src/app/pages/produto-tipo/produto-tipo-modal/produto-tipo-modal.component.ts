import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProdutoTipoListaResposta } from '../../../core/services/produto-tipo.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
    selector: 'app-produto-tipo-dialog',
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatSnackBarModule],
    templateUrl: './produto-tipo-modal.component.html'
})

export class ProdutoTipoModalComponent {
    private readonly fb = inject(FormBuilder);
    private readonly dialogRef = inject(MatDialogRef<ProdutoTipoModalComponent>);
    public data: ProdutoTipoListaResposta | null = inject(MAT_DIALOG_DATA);
    editando = !!this.data;

    formulario: FormGroup = this.fb.group({
        idProdutoTipo: [this.data?.idProdutoTipo ?? 0],
        nmProdutoTipo: [this.data?.nmProdutoTipo, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]]
    });

    salvar(): void {
        if (this.formulario.valid) this.dialogRef.close(this.formulario.value);
    }

    fechar(): void {
        this.dialogRef.close();
    }
}