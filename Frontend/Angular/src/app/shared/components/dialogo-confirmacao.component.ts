import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

export interface ConfirmDialogData {
    titulo?: string;
    mensagem?: string;
    textoConfirmar?: string;
    textoCancelar?: string;
    corBotao?: 'primary' | 'warn';
}

@Component({
    selector: 'app-dialogo-confirmacao',
    standalone: true,
    imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule],
    templateUrl: './dialogo-confirmacao.component.html'
})

export class DialogoConfirmacaoComponent {
    private readonly dialogoRef = inject(MatDialogRef<DialogoConfirmacaoComponent>);
    readonly dado: ConfirmDialogData = inject(MAT_DIALOG_DATA);

    confirmar(): void {
        this.dialogoRef.close(true);
    }

    cancelar(): void {
        this.dialogoRef.close(false);
    }
}