import { ApplicationConfig, provideBrowserGlobalErrorListeners, LOCALE_ID } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { autenticacaoInterceptor } from './core/interceptors/autenticacao.interceptor';
import { routes } from './app.routes';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { PtBrPaginatorIntl } from './shared/i18n/pt-br-paginator-intl';

export const appConfig: ApplicationConfig = {
    providers: [
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes),
        provideHttpClient(withInterceptors([autenticacaoInterceptor])),
        { provide: MatPaginatorIntl, useClass: PtBrPaginatorIntl },
        { provide: LOCALE_ID, useValue: 'pt-BR' }
    ]
};