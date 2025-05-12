import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { interval, Observable, Subscription } from 'rxjs';
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
  // This variable is used to store the current index of the image
  currentImageIndex = 0;
  private intervalSub!: Subscription;

  // This variable is used to store the game details
  game$: Observable<Game | null> = this.store.select(selectGameDetails);;
  loading$: Observable<boolean> = this.store.select(selectGameDetailsLoading);
  error$: Observable<string | null> = this.store.select(selectGameDetailsError);
  ngOnInit() {

    const gameId = this.route.snapshot.paramMap.get('id');
    if (gameId && !isNaN(+gameId)) {
      this.store.dispatch(loadGameDetails({ id: +gameId }));
      this.game$.subscribe(game => {
        if (game?.imagesUrls?.length) {
          this.startAutoIndex(game.imagesUrls.length);
        }
      });
    }
  }

  // This function updates the index over time and returns the current index
  startAutoIndex(length: number): void {
    if (this.intervalSub) {
      this.intervalSub.unsubscribe();
    }
    this.intervalSub = interval(5000).subscribe(() => {
      this.currentImageIndex = (this.currentImageIndex + 1) % length;
    });
  }
  // Cleanup
  ngOnDestroy(): void {
    if (this.intervalSub) {
      this.intervalSub.unsubscribe();
    }
  }
  // This function is used to get the image url
  getImageUrl(Url: any | null): any {
    return imagePath(Url);
  }
  // This function is used to change the image index
  changeImage(index: number): void {
    this.currentImageIndex = index;
  }
}
