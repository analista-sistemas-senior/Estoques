import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FornecedorListaResposta } from '../../../core/services/fornecedor.service';

@Component({
    selector: 'app-fornecedor-dialog',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
    templateUrl: './fornecedor-modal.component.html'
})

export class FornecedorModalComponent {
    private readonly fb = inject(FormBuilder);
    private readonly dialogRef = inject(MatDialogRef<FornecedorModalComponent>);
    public data: FornecedorListaResposta | null = inject(MAT_DIALOG_DATA);

    editando = !!this.data;

    formulario: FormGroup = this.fb.group({
        idFornecedor: [this.data?.idFornecedor ?? 0],
        nmFornecedor: [this.data?.nmFornecedor, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
        txEndereco: [this.data?.txEndereco, [Validators.minLength(3), Validators.maxLength(255)]],
        txAnotacao: [this.data?.txAnotacao, [Validators.minLength(3), Validators.maxLength(1024)]]
    });

    salvar(): void {
        if (this.formulario.valid) this.dialogRef.close(this.formulario.value);
    }

    fechar(): void {
        this.dialogRef.close();
    }
}