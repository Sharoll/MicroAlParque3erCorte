import { Component, OnInit } from '@angular/core';
import { RestaurantesService } from 'src/app/services/restaurantes.service';
import { Restaurantes } from '../models/restaurantes';

@Component({
  selector: 'app-restaurante-consulta',
  templateUrl: './restaurante-consulta.component.html',
  styleUrls: ['./restaurante-consulta.component.css']
})
export class RestauranteConsultaComponent implements OnInit {

  restaurantes:Restaurantes[];
  searchText: string;
  constructor(private restauranteService: RestaurantesService) { }

  ngOnInit(): void {
    this.consultaRestaurante();
  }
  consultaRestaurante(){
    this.restauranteService.get().subscribe(result=>{
      this.restaurantes =result;
    })
  }

}
