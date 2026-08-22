import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, EMPTY, throwError } from 'rxjs';

export const autenticacaoInterceptor: HttpInterceptorFn = (requisicao, next) => {
    const router = inject(Router);
    const token = typeof window !== 'undefined' ? localStorage.getItem('usuario_token') : null;
    const snackBar = inject(MatSnackBar);
    const rotaLogin = requisicao.url.includes('/login');

    let requisicaoClonada = requisicao;
    if (token) requisicaoClonada = requisicao.clone({ setHeaders: { Authorization: `Bearer ${token}` }});

    return next(requisicaoClonada).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status === 401 && !rotaLogin) {
                if (typeof window !== 'undefined') localStorage.removeItem('usuario_token');
                snackBar.open('Sua sessão expirou. Faça login novamente.', 'OK', { duration: 3000, horizontalPosition: 'center', verticalPosition: 'top' });
                router.navigate(['/login']);
                return EMPTY;
            }
            return throwError(() => error);
        })
    );
};