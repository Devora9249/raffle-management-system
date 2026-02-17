// import { inject } from "@angular/core";
// import { CanMatchFn, Router } from "@angular/router";
// import { catchError, map, of } from "rxjs";
// import { RaffleService } from "../services/RaffleService";

// export const raffleRedirectGuard: CanMatchFn = () => {
//   const raffleService = inject(RaffleService);
//   const router = inject(Router);

//   return raffleService.getStatus().pipe(
//     map(res => {
//       if (res.finished) {
//         return router.createUrlTree(['/winnings']);
//       }

//       return true; // מאפשר redirectTo: 'home'
//     }),
//     catchError(() => of(true))
//   );
// };


import { inject } from "@angular/core";
import { CanActivateFn, Router, UrlTree } from "@angular/router";
import { catchError, map, of, take, Observable } from "rxjs";
import { WinningService } from "../services/winning-service";

export const raffleRedirectGuard: CanActivateFn = (): Observable<boolean | UrlTree> => {
  const winningService = inject(WinningService);
  const router = inject(Router);

  return winningService.getStatus().pipe(
    take(1),
    map(res => {
      console.log("Raffle status received in guard:", res);
      if (res) {
        return router.createUrlTree(['/winnings']);
      }
      return true;
    }),
    catchError(() => {
      return of(true);
    })
  );
};