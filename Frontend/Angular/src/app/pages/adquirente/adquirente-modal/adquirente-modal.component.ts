import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AdquirenteListaResposta } from '../../../core/services/adquirente.service';

@Component({
    selector: 'app-adquirente-dialog',
    imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
    templateUrl: './adquirente-modal.component.html'
})

export class AdquirenteModalComponent {
    private readonly fb = inject(FormBuilder);
    private readonly dialogRef = inject(MatDialogRef<AdquirenteModalComponent>);
    data: AdquirenteListaResposta | null = inject(MAT_DIALOG_DATA);
    editando = !!this.data;

    formulario: FormGroup = this.fb.group({
        idAdquirente: [this.data?.idAdquirente ?? 0],
        nmAdquirente: [this.data?.nmAdquirente, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
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