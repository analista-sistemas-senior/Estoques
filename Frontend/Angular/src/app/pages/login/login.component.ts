import { Component, ElementRef, OnInit, ViewChild, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AutenticacaoService } from '../../core/services/autenticacao.service';

@Component({
    selector: 'app-login',
    imports: [RouterLink, CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatProgressBarModule, MatSnackBarModule],
    templateUrl: './login.component.html'
})

export class LoginComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly router = inject(Router);
    private readonly autenticacaoService = inject(AutenticacaoService);
    private readonly snackBar = inject(MatSnackBar);
    loginFormulario!: FormGroup;
    carregando = signal<boolean>(false);

    @ViewChild('nmLogin') inputNome!: ElementRef<HTMLInputElement>;

    ngOnInit(): void {
        this.loginFormulario = this.fb.group({
            nmLogin: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
            cdSenha: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(255)]]
        });
    }

    onSubmit(): void {
        if (this.loginFormulario.valid) {
            this.carregando.set(true);
            const { nmLogin, cdSenha } = this.loginFormulario.value;
            this.autenticacaoService.login(nmLogin, cdSenha).subscribe({
                next: () => {
                    this.carregando.set(false);
                    this.snackBar.open('Login realizado com sucesso', 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                    this.router.navigate(['/dashboard']);
                },
                error: (erro) => {
                    this.carregando.set(false);
                    this.snackBar.open('Nome de usuário ou senha incorretos', 'Fechar', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                }
            });
        }
    }

    ngAfterViewInit(): void {
        setTimeout(() => { this.inputNome.nativeElement.focus(); }, 500);
    }
}