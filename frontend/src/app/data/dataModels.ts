export class AppDataObject {
    Id: number;
    RID: string;
    Registro: Date;
    DataUpdate: Date;
    Ativo: boolean;
}
export class CadastroUsuario {
    NomeUsuario: string;
    Email: string;
    Senha: string;
    Csenha: string;
}
export class PermissaoUsuario extends AppDataObject {
    UsuarioId: number;
    Usuario?: Object;
    Component: number;
    Consultar: boolean;
    Incluir: boolean;
    Editar: boolean;
    Excluir: boolean;
}

//comunicationModels
export interface IResponse {
    Sucesso: boolean;
    Mensagem?: string;
}
export class LoginResponseModel {
    UserName: string;
    Sucesso: boolean = false;
    Mensagem: string;
    Token: string;
}
export class SessaoUsuario {
    Token: string = '';
    Mensagem: string;
    Sucesso: boolean = false;
    UserName: string;
    UserId: number;
    UserRID: string;
    PermissoesUser: Array<PermissaoUsuario>;
}
export class DefaultResponseModel {
    Sucesso: boolean = false;
    Mensagem: string;
    Retorno?: Object;
}
export interface IComponent{
    Id:number;
    Descricao:string;
    Route?:string;
}
export interface IRoute{
    Descricao:string;
    Link:string;
}
export const DATA_SOURCE_COMPONENTS: IComponent[] = [
    { Id: 6731, Descricao: "admin", Route: '/Admin' },
    { Id: 6908, Descricao: "escalas", Route: '/Admin/Escalas' },
    { Id: 3233, Descricao: "especialidades", Route: '/Admin/Especialidades' },
    { Id: 2219, Descricao: "medicos", Route: '/Admin/Medicos' },
    { Id: 1058, Descricao: "setores", Route: '/Admin/Setores' },
    { Id: 4486, Descricao: "unidades-atendimento", Route: '/Admin/UnidadedesDeAtentimento' }
];