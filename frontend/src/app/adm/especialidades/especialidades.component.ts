import { Component, OnInit } from '@angular/core';
import { AdmComponent } from '../adm.component';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-especialidades',
  templateUrl: './especialidades.component.html',
  styleUrls: ['./especialidades.component.scss']
})
export class EspecialidadesComponent implements OnInit {

  public modelF1: any = {};
  public modelF2: any = {};
  public modelF3: any = { Unidade: {}, Especialidade: {} };
  public lista: Array<any> = [];
  public unidades: Array<any> = [];
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

  novoItem(op: number = 1) {

    if (op == 1) {

      this.novo = true;
      this.editar = false;
      this.modelF1 = {};

    } else {
      this.novo2 = true;
      this.editar2 = false;
      this.modelF2 = {};
    }

  }

  //carregando unidades e setores
  async atualizar() {

    this.unidades = await this.servidor.chamarApi("api/Unidades", null);
    this.lista  = await this.servidor.chamarApi('api/Especialidades', null);

  }

  changeSelect() {

    this.lista.forEach(espec => {
      if (espec.rid == this.modelF2.Especialidade) {
        this.modelF3.Especialidade = espec;
      }
    });
    this.unidades.forEach(unidade => {
      if (unidade.rid == this.modelF2.Unidade) {
        this.modelF3.Unidade = unidade;
      }
    });

  }

  async salvarItem(op: number = 1) {


    if (op == 1) {

      this.novo = false;
      this.editar = false;

      if (Object.keys(this.modelF1).length == 0) {
        return;
      }

      try {
        const response: any = await this.servidor.chamarApi('api/Especialidades', this.modelF1);

        if (response.Sucesso) {
          this.salvando = false;
          this.modelF1 = {};
          this.atualizar();
        }

      } catch (err) {
        this.salvando = false;
        this.modelF1 = {};
        alert(err.mensagem);
      }

    } else {

      this.novo2 = false;
      this.editar2 = false;

      if (Object.keys(this.modelF2).length < 2) {
        return;
      }

      try {
        const response: any = await this.servidor.chamarApi('api/EspecialidadesUnidades', this.modelF3);

        if (response.Sucesso) {
          this.salvando = false;
          this.modelF2 = {};
          this.modelF2 = { Unidade: "", Especialidade: "" };
          this.atualizar();
        }

      } catch (err) {
        this.salvando = false;
        alert(err.mensagem);
      }

    }


  }

  async excluirItem(op: number = 1) {
    if (op == 1) {
      if (this.editar) {

        if (confirm("Deseja Realmente Excluir?")) {
          this.editar = false;

          try {
            const response: any = await this.servidor.chamarApi('api/Especialidades/Delete', this.modelF1);

            if (response.Sucesso) {
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
    }
  }

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
