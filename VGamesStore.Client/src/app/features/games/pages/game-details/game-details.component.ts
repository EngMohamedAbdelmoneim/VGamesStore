import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Game } from '../../../../core/models/game';
import { selectGameDetails, selectGameDetailsError, selectGameDetailsLoading } from '../../store/game.selectors';
import { loadGameDetails } from '../../store/game.actions';
import { CommonModule } from '@angular/common';
import { imagePath } from '../../../../utils/imagePath';


@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  imports: [CommonModule],
  styleUrls: ['./game-details.component.css']
})
export class GameDetailsComponent implements OnInit {

  private store = inject(Store);
  private route = inject(ActivatedRoute);

  game$: Observable<Game | null> = this.store.select(selectGameDetails);;
  loading$: Observable<boolean> = this.store.select(selectGameDetailsLoading);
  error$: Observable<string | null> = this.store.select(selectGameDetailsError);
  ngOnInit() {

    const gameId = this.route.snapshot.paramMap.get('id');
    console.log('Game ID from route:', gameId);
    if (gameId && !isNaN(+gameId)) {
      this.store.dispatch(loadGameDetails({ id: +gameId }));
      console.log('Game Load' , this.game$);
    } else {
      console.error('Invalid game ID:', gameId);
    }
  }
  getFormattedPrice(Url:any | null): any {
      return imagePath(Url);
    }
}
