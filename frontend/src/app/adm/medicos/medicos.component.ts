import { Component, OnInit } from '@angular/core';
import { AdmComponent } from '../adm.component';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-medicos',
  templateUrl: './medicos.component.html',
  styleUrls: ['./medicos.component.scss']
})
export class MedicosComponent implements OnInit {

  public modelF1: any = {};
  public modelF2: any = {};
  public modelF3: any = { Unidade: {}, Medico: {}, SetorUnidade: {}, EspecialidadeUnidade: {} };

  public unidades: Array<any> = [];

  public medicos: Array<any> = [];
  public especialidades: Array<any> = [];
  public setores: Array<any> = [];

  public especialidadesUnd: Array<any> = [];
  public setoresUnd: Array<any> = [];

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
    this.medicos = await this.servidor.chamarApi('api/Medicos', null);
  }

  changeSelect(op: number = 0) {

    if (op == 1) {
      this.modelF3 = { Unidade: {}, Medico: {}, SetorUnidade: {}, EspecialidadeUnidade: {} };

      this.unidades.forEach(unidade => {
        if (unidade.id == this.modelF2.Unidade) {
          this.modelF3.Unidade = unidade;
          this.especialidades = unidade.especialidades;
          this.setores = unidade.setores;
        }
      });
    } else {

      this.medicos.forEach(medico => {
        if (medico.id == this.modelF2.Medico) {
          this.modelF3.Medico = medico;
        }
      });

      this.unidades.forEach(unidade => {

        if (unidade.id == this.modelF2.Unidade) {

          unidade.setores.forEach(setoresunidade => {
            if (setoresunidade.id == this.modelF2.SetorUnidade) {
              this.modelF3.SetorUnidade = setoresunidade;
            }
          });

          unidade.especialidades.forEach(espec => {
            if (espec.id == this.modelF2.EspecialidadeUnidade) {
              this.modelF3.EspecialidadeUnidade = espec;

            }
          });

        }

      });

    }

  }

  async salvarItem(op: number = 1) {


    if (op == 1) {

      this.novo = false;
      this.editar = false;

      if (Object.keys(this.modelF1).length == 0) {
        return;
      }

      try {
        const response: any = await this.servidor.chamarApi('api/Medicos', this.modelF1);
 
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

      if (Object.keys(this.modelF2).length < 0) {
        return;
      }
      this.novo2 = false;
      this.editar2 = false;

      try {
        const response: any = await this.servidor.chamarApi('api/MedicosUnidades', this.modelF3);

        if (response.Sucesso) {
          this.salvando = false;
          this.modelF2 = {};
          this.modelF3 = {};
          this.atualizar();
        }
      } catch (err) {
        this.salvando = false;
        this.modelF2 = {};
        this.modelF3 = {};
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
            const response: any = await this.servidor.chamarApi('api/Medicos/Delete', this.modelF1);
            
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
        }

      }
    } else {
      if (this.editar2) {

        if (confirm("Deseja Realmente Excluir?")) {
          this.editar2 = false;

          try {
            const response:any = await this.servidor.chamarApi('api/MedicosUnidades/Delete', this.modelF3);

            if (response.Sucesso) {
              this.salvando = false;
              this.modelF3 = { Unidade: {}, Medico: {}, SetorUnidade: {}, EspecialidadeUnidade: {} };
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

  editarSubItem(medico: any) {

    this.novo2 = true;
    this.editar2 = true;

    this.modelF2 = {
      Unidade: medico.unidadeId,
      Medico: medico.medico.id,
      SetorUnidade: medico.setorUnidade.id,
      EspecialidadeUnidade: medico.especialidadeUnidade.id
    };
    this.changeSelect(1);
    this.changeSelect(2);

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
