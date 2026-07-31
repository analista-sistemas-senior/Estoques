import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { registerLocaleData } from '@angular/common';
import localPtBr from '@angular/common/locales/pt';

registerLocaleData(localPtBr, 'pt-BR');
bootstrapApplication(App, appConfig).catch((erro) => console.error(erro));