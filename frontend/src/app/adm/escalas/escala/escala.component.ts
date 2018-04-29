import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdmComponent } from '../../adm.component';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-escala',
  templateUrl: './escala.component.html',
  styleUrls: ['./escala.component.scss']
})
export class EscalaComponent implements OnInit {

  public id: string = "";
  public salvando: boolean = false;
  public novo: boolean = false;
  public editar: boolean = false;

  public model: any = {};
  public modelEdit: any = {};
  public modelSave: any = { Unidade: {}, SetorUnidade: {}, EspecialidadeUnidade: {}, MedicoUnidade: {}, Horario_inicial: "", Horario_Final: "" };

  public unidades: Array<any> = [];
  public setoresUnidade: Array<any> = [];
  public especialidadesUnidade: Array<any> = [];
  public medicosUnidade: Array<any> = [];
  public lista: Array<any> = [];

  constructor(private router: ActivatedRoute, private admComponent: AdmComponent, private servidor: ApiService) { }


  ngOnInit() {

    this.atualizar();

    this.router.params.subscribe( async params => {

      this.id = params['escala'];

      if (this.id == "Nova") {

        this.novoItem();

      } else {

        try {
          const response: any =  await this.servidor.chamarApi('api/Plantoes/' + this.id, null);
   
          let model: any = response;

          this.model = {
            Unidade: model.unidade.id,
            Horario_Inicial: model.hora_Inicial,
            Horario_Final: model.hora_Final,
            Data: model.data,
            SetorUnidade: model.setorUnidade.id,
            EspecialidadeUnidade: model.especialidadeUnidade.id,
            MedicoUnidade: model.medicosUnidade.id
          };

          this.unidades.forEach(unidade => {

            if (unidade.id == this.model.Unidade) {

              this.setoresUnidade = unidade.setores;
              this.especialidadesUnidade = unidade.especialidades;

              this.modelSave.Unidade = unidade;
              this.modelSave.Horario_Inicial = this.model.Horario_Inicial;
              this.modelSave.Horario_Final = this.model.Horario_Final;
              this.modelSave.Data = this.model.Data;

              unidade.medicos.forEach(medicoUnidade => {

                if (medicoUnidade.setorUnidadeId == this.model.SetorUnidade && medicoUnidade.especialidadeUnidadeId == this.model.EspecialidadeUnidade) {
                  this.medicosUnidade.push(medicoUnidade);
                }

              });

            }

          });


        } catch (err) {
          this.salvando = false;
          alert(err.mensagem);
        }

      }

    });

  }

  novoItem() {
    this.novo = true;
    this.editar = false;
    this.model = {};
  }

  async atualizar() {
    this.unidades = await this.servidor.chamarApi("api/Unidades", null);
  }

  changeSelect(op: number = 0, medU: boolean = true) {

    if (op == 1) {

      this.modelSave = { Unidade: {}, SetorUnidade: {}, EspecialidadeUnidade: {}, MedicoUnidade: {}, Horario_Inicial: "", Horario_Final: "", Data: "" };

      this.unidades.forEach(unidade => {

        if (unidade.id == this.model.Unidade) {

          this.setoresUnidade = unidade.setores;
          this.especialidadesUnidade = unidade.especialidades;
          this.modelSave.Unidade = unidade;
          this.modelSave.Horario_Inicial = this.model.Horario_Inicial;
          this.modelSave.Horario_Final = this.model.Horario_Final;
          this.modelSave.Data = this.model.Data;

        }

      });

    } else {

      this.unidades.forEach(unidade => {

        if (unidade.id == this.model.Unidade) {

          unidade.setores.forEach(setorUnidade => {

            if (setorUnidade.id == this.model.SetorUnidade) {
              this.modelSave.SetorUnidade = setorUnidade;
            }

          });

          unidade.especialidades.forEach(especUnidade => {

            if (especUnidade.id == this.model.EspecialidadeUnidade) {
              this.modelSave.EspecialidadeUnidade = especUnidade;
            }

          });

          if (medU) {
            this.medicosUnidade = [];

            unidade.medicos.forEach(medicoUnidade => {

              if (medicoUnidade.setorUnidadeId == this.model.SetorUnidade && medicoUnidade.especialidadeUnidadeId == this.model.EspecialidadeUnidade) {
                this.medicosUnidade.push(medicoUnidade);
              }

            });
          }

          unidade.medicos.forEach(medicoUnidade => {

            if (medicoUnidade.id == this.model.MedicoUnidade) {
              this.modelSave.MedicoUnidade = medicoUnidade;
            }

          });

        }

      });

    }

  }



  async salvarItem() {
    this.novo = false;
    this.editar = false;

    if (Object.keys(this.model).length == 0) {
      return;
    }
    this.modelSave.Horario_Inicial = this.model.Horario_Inicial;
    this.modelSave.Horario_Final = this.model.Horario_Final;
    this.modelSave.Data = this.model.Data;

    try {
      const response: any = await this.servidor.chamarApi('api/Plantoes', this.modelSave);
   
      if (response.Sucesso) {
        this.salvando = false;
        this.atualizar();
        this.carregarLista(this.model);
        this.model = {};
      }
    } catch (err) {
      this.salvando = false;
      alert(err.mensagem);
    }

  }

  carregarLista(item: any) {
    this.lista[0] = this.modelSave;
    this.modelEdit = item;
  }

  async excluirItem() {

    if (this.editar) {

      if (confirm("Deseja Realmente Excluir?")) {
        this.editar = false;
        try {
          const response: any = await this.servidor.chamarApi('api/Plantoes/Delete', this.modelSave);

          if (response.Sucesso) {
            this.salvando = false;
            this.model = {};
            this.atualizar();
          }
        } catch (err) {
          this.salvando = false;
          alert(err.mensagem);
        }
      }

    }
  }

  editarItem() {
    this.novo = true;
    this.editar = true;
    this.model = this.modelEdit;
    this.changeSelect(1);
    this.changeSelect();
  }

}
