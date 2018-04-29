import { Component, OnInit } from '@angular/core';
import { AdmComponent } from '../adm.component';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-setores',
  templateUrl: './setores.component.html',
  styleUrls: ['./setores.component.scss']
})
export class SetoresComponent implements OnInit {

  public modelF1: any = {};
  public modelF2: any = { Unidade: "", Setor: "" };
  public modelF3: any = { Unidade: {}, Setor: {} };

  public lista: Array<any> = [];
  public unidades: Array<any> = [];
  public setores: Array<any> = [];
  public salvando: boolean = false;

  public novo: boolean = false;
  public editar: boolean = false;
  public novo2: boolean = false;
  public editar2: boolean = false;

  public atual: number = -1;
  public cont: number = 1;

  constructor(private admComponent: AdmComponent, private servidor: ApiService) { }


  ngOnInit() {
    this.atualizar();
  }

  //novo item setor ou setor Unidade
  novoItem(op: number = 1) {

    if (op == 1) {

      this.novo = true;
      this.editar = false;
      this.modelF1 = {};

    } else {
      this.novo2 = true;
      this.editar2 = false;
      this.modelF2 = { Unidade: "", Setor: "" };
    }

  }

  //carregando unidades e setores
  async atualizar() {

    this.unidades = await this.servidor.chamarApi("api/Unidades", null);
    let responseSetores:any = await this.servidor.chamarApi('api/Setores', null); 
    
    this.setores = JSON.parse(responseSetores._body);

  }

  //alteração no combobox
  changeSelect() {

    this.setores.forEach(setor => {
      if (setor.rid == this.modelF2.Setor) {
        this.modelF3.Setor = setor;
      }
    });
    this.unidades.forEach(unidade => {
      if (unidade.rid == this.modelF2.Unidade) {
        this.modelF3.Unidade = unidade;
      }
    });

  }

  //salvando item
  async salvarItem(op: number = 1) {


    if (op == 1) {

      this.novo = false;
      this.editar = false;

      if (Object.keys(this.modelF1).length == 0) {
        return;
      }

        const response: any = await this.servidor.chamarApi('api/Setores', this.modelF1);

        if (response.sucesso) {
          this.salvando = false;
          this.modelF1 = {};
          this.atualizar();
        }else{
          this.salvando = false;
          this.modelF1 = {};
          
        }

    } else {

      this.novo2 = false;
      this.editar2 = false;

      if (Object.keys(this.modelF2).length < 2) {
        return;
      }

        const response: any = await this.servidor.chamarApi('api/SetoresUnidades', this.modelF3);
   
        if (response.Sucesso) {
          this.salvando = false;
          this.modelF2 = {};
          this.modelF3 = {};
          this.atualizar();
        }else{
          this.salvando = false;
          this.modelF2 = {};
          this.modelF3 = {};
        }


    }


  }

  //excluindo item
  async excluirItem(op: number = 1) {
    if (op == 1) {
      if (this.editar) {

        if (confirm("Deseja Realmente Excluir?")) {
          this.editar = false;

          try {
            const post: any = await this.servidor.chamarApi('api/Setores/Delete', this.modelF1);
            let response:any = JSON.parse(post._body);

            if (response.sucesso) {
              this.salvando = false;
              this.modelF1 = {};
              this.atualizar();
            }

          } catch (err) {
            this.salvando = false;
            alert(err.mensagem);
          }

        }

      }
    } else {
      if (this.editar2) {

        if (confirm("Deseja Realmente Excluir?")) {
          this.editar2 = false;

          try {
            const post: any = await this.servidor.chamarApi('api/SetoresUnidades/Delete', this.modelF2);
            let response:any = JSON.parse(post._body);

            if (response.sucesso) {
              this.salvando = false;
              this.modelF2 = {};
              this.atualizar();
            }

          } catch (err) {
            this.salvando = false;
            alert(err.mensagem);
          }
        }

      }
    }
  }

  //editando item
  editarItem(item: any, op: number = 1) {

    if (op == 1) {
      this.novo = true;
      this.editar = true;

      this.modelF1 = item;
    } else {
      this.novo2 = true;
      this.editar2 = true;

    }

  }

  abrir(i: number) {
    if (this.cont == 1) {
      this.atual = i;
      this.cont = 0;
    } else {
      this.atual = -1;
      this.cont = 1;
    }
  }

}
