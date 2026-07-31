import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface FornecedorListaResposta {
    idFornecedor: number;
    nmFornecedor: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface FornecedorCadastroRequisicao {
    nmFornecedor: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface FornecedorCadastroResposta {
    idFornecedor: number;
    nmFornecedor: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface FornecedorAtualizacaoRequisicao {
    idFornecedor: number;
    nmFornecedor: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface FornecedorExclusaoRequisicao {
    idFornecedor: number;
}

@Injectable({ providedIn: 'root' })
export class FornecedorService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/fornecedores`;

    listar(): Observable<FornecedorListaResposta[]> {
        return this.http.get<FornecedorListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: FornecedorCadastroRequisicao): Observable<FornecedorCadastroResposta> {
        return this.http.post<FornecedorCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: FornecedorAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idFornecedor: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idFornecedor}`);
    }
}