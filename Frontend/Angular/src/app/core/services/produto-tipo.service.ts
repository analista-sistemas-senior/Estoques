import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ProdutoTipoListaResposta {
    idProdutoTipo: number;
    nmProdutoTipo: string;
}
export interface ProdutoTipoCadastroRequisicao {
    nmProdutoTipo: string;
}
export interface ProdutoTipoCadastroResposta {
    idProdutoTipo: number;
    nmProdutoTipo: string;
}
export interface ProdutoTipoAtualizacaoRequisicao {
    idProdutoTipo: number;
    nmProdutoTipo: string;
}
export interface ProdutoTipoExclusaoRequisicao {
    idProdutoTipo: number;
}

@Injectable({ providedIn: 'root' })
export class ProdutoTipoService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/produtos/tipos`;

    listar(): Observable<ProdutoTipoListaResposta[]> {
        return this.http.get<ProdutoTipoListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: ProdutoTipoCadastroRequisicao): Observable<ProdutoTipoCadastroResposta> {
        return this.http.post<ProdutoTipoCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: ProdutoTipoAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idProdutoTipo: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idProdutoTipo}`);
    }
}