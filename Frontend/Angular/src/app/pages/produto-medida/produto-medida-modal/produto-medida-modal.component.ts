import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProdutoMedidaListaResposta } from '../../../core/services/produto-medida.service';

@Component({
    selector: 'app-produto-medida-dialog',
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
    templateUrl: './produto-medida-modal.component.html'
})

export class ProdutoMedidaModalComponent {
    private readonly fb = inject(FormBuilder);
    private readonly dialogRef = inject(MatDialogRef<ProdutoMedidaModalComponent>);
    data: ProdutoMedidaListaResposta | null = inject(MAT_DIALOG_DATA);
    editando = !!this.data;

    formulario: FormGroup = this.fb.group({
        idProdutoMedida: [this.data?.idProdutoMedida ?? 0],
        mdProdutoMedida: [this.data?.mdProdutoMedida, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]]
    });

    salvar(): void {
        if (this.formulario.valid) this.dialogRef.close(this.formulario.value);
    }

    fechar(): void {
        this.dialogRef.close();
    }
}