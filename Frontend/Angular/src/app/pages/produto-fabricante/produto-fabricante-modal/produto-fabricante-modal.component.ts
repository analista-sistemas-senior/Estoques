import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProdutoFabricanteListaResposta } from '../../../core/services/produto-fabricante.service';

@Component({
    selector: 'app-produto-fabricante-dialog',
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
    templateUrl: './produto-fabricante-modal.component.html'
})

export class ProdutoFabricanteModalComponent {
    private readonly fb = inject(FormBuilder);
    private readonly dialogRef = inject(MatDialogRef<ProdutoFabricanteModalComponent>);
    data: ProdutoFabricanteListaResposta | null = inject(MAT_DIALOG_DATA);
    editando = !!this.data;

    formulario: FormGroup = this.fb.group({
        idProdutoFabricante: [this.data?.idProdutoFabricante ?? 0],
        nmProdutoFabricante: [this.data?.nmProdutoFabricante, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]]
    });

    salvar(): void {
        if (this.formulario.valid) this.dialogRef.close(this.formulario.value);
    }

    fechar(): void {
        this.dialogRef.close();
    }
}