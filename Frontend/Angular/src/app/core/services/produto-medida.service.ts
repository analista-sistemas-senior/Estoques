import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ProdutoMedidaListaResposta {
    idProdutoMedida: number;
    mdProdutoMedida: string;
}
export interface ProdutoMedidaCadastroRequisicao {
    mdProdutoMedida: string;
}
export interface ProdutoMedidaCadastroResposta {
    idProdutoMedida: number;
    mdProdutoMedida: string;
}
export interface ProdutoMedidaAtualizacaoRequisicao {
    idProdutoMedida: number;
    mdProdutoMedida: string;
}
export interface ProdutoMedidaExclusaoRequisicao {
    idProdutoMedida: number;
}

@Injectable({ providedIn: 'root' })
export class ProdutoMedidaService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/produtos/medidas`;

    listar(): Observable<ProdutoMedidaListaResposta[]> {
        return this.http.get<ProdutoMedidaListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: ProdutoMedidaCadastroRequisicao): Observable<ProdutoMedidaCadastroResposta> {
        return this.http.post<ProdutoMedidaCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: ProdutoMedidaAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idProdutoMedida: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idProdutoMedida}`);
    }
}