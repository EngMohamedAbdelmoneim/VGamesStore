
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameCardComponent } from "../../../games/components/game.card/game.card.component";
import { SearchListComponent } from "../../components/search-list/search-list/search-list.component";
import { RouterLink } from '@angular/router';
import { FilterDtoComponent } from "../../components/filter/filter.component";

@Component({
  selector: 'app-search-games-list-page',
  imports: [CommonModule, SearchListComponent, FilterDtoComponent],
  templateUrl: './search-games-list-page.component.html',
  styleUrl: './search-games-list-page.component.css'
})
export class SearchGamesListPageComponent implements OnInit {
  ngOnInit(): void {}
}
