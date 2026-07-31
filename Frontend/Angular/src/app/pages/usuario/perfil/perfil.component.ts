import { Component, ElementRef, OnInit, ViewChild, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UsuarioService } from '../../../core/services/usuario.service';
import { AutenticacaoService } from '../../../core/services/autenticacao.service';

@Component({
    selector: 'app-usuario-perfil',
    imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatProgressBarModule, MatSnackBarModule],
    templateUrl: './perfil.component.html'
})

export class UsuarioPerfilComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly router = inject(Router);
    private readonly snackBar = inject(MatSnackBar);
    private readonly usuarioService = inject(UsuarioService);
    private readonly autenticacaoService = inject(AutenticacaoService);
    readonly usuario = this.autenticacaoService.usuarioDados;
    usuarioPerfilFormulario!: FormGroup;
    carregando = signal<boolean>(false);

    @ViewChild('nmUsuario') inputNome!: ElementRef<HTMLInputElement>;

    ngOnInit(): void {
        this.usuarioPerfilFormulario = this.fb.group(
            {
                nmUsuario: [this.usuario()?.nmUsuario, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
                nmLogin: [{ value: this.usuario()?.nmLogin, disabled: true }, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
                cdSenha: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(40)]],
                cdSenhaConfirmacao: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(40)]]
            },
            { 
                validators: this.validarSenhasIguais
            }
        );
    }

    onSubmit(): void {
        if (this.usuarioPerfilFormulario.valid) {
            this.carregando.set(true);
            const { idUsuario, nmUsuario, nmLogin, cdSenha } = this.usuarioPerfilFormulario.getRawValue();
            this.usuarioService.atualizar({ idUsuario, nmUsuario, nmLogin, cdSenha }).subscribe({
                next: () => {
                    this.carregando.set(false);
                    this.snackBar.open('Perfil atualizado com sucesso', 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                    this.autenticacaoService.atualizarUsuario({ nmUsuario: nmUsuario });
                    this.router.navigate(['/dashboard']);
                },
                error: (erro) => {
                    this.carregando.set(false);
                    this.snackBar.open(erro.error.mensagem ?? 'Sem resposta do servidor', 'Fechar', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                }
            });
        }
    }

    ngAfterViewInit(): void {
        setTimeout(() => { this.inputNome.nativeElement.focus(); }, 500);
    }

    private validarSenhasIguais(control: AbstractControl): ValidationErrors | null {
        const senha = control.get('cdSenha')?.value;
        const confirmacao = control.get('cdSenhaConfirmacao')?.value;

        if (senha && confirmacao && senha !== confirmacao) {
            control.get('cdSenhaConfirmacao')?.setErrors({ senhasDiferentes: true });
            return { senhasDiferentes: true };
        }

        return null;
    }
}