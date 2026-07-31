import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ProdutoFabricanteListaResposta {
    idProdutoFabricante: number;
    nmProdutoFabricante: string;
}
export interface ProdutoFabricanteCadastroRequisicao {
    nmProdutoFabricante: string;
}
export interface ProdutoFabricanteCadastroResposta {
    idProdutoFabricante: number;
    nmProdutoFabricante: string;
}
export interface ProdutoFabricanteAtualizacaoRequisicao {
    idProdutoFabricante: number;
    nmProdutoFabricante: string;
}
export interface ProdutoFabricanteExclusaoRequisicao {
    idProdutoFabricante: number;
}

@Injectable({ providedIn: 'root' })
export class ProdutoFabricanteService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/produtos/fabricantes`;

    listar(): Observable<ProdutoFabricanteListaResposta[]> {
        return this.http.get<ProdutoFabricanteListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: ProdutoFabricanteCadastroRequisicao): Observable<ProdutoFabricanteCadastroResposta> {
        return this.http.post<ProdutoFabricanteCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: ProdutoFabricanteAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idProdutoFabricante: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idProdutoFabricante}`);
    }
}