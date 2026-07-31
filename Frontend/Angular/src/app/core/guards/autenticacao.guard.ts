import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AutenticacaoService } from '../services/autenticacao.service';

export const autenticacaoGuard: CanActivateFn = (route, state) => {
    const autenticacaoService = inject(AutenticacaoService);
    const router = inject(Router);

    if (autenticacaoService.autenticado()) return true; 

    router.navigate(['/login']);

    return false;
};