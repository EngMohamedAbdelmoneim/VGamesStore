import { CommonModule } from '@angular/common';
import { imagePath } from '../../../../utils/imagePath';
import { Component, Input } from '@angular/core';
import { Game } from '../../../../core/models/game';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-game-card',
  imports: [CommonModule,RouterLink],
  templateUrl: './game.card.component.html',
  standalone: true,
  styleUrl: './game.card.component.css'
})

export class GameCardComponent {
@Input() game!: Game;
getFormattedPrice(Url:any | null): any {
    return imagePath(Url);
  }
}
