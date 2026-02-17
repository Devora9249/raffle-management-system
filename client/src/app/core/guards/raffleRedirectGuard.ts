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
    take(1), // חשוב מאוד כדי שהגארד יסיים את הריצה
    map(res => {
      // אם ההגרלה הסתיימה, נווט לעמוד הזכיות
      if (res && res.finished) {
        return router.createUrlTree(['/winnings']);
      }
      // אחרת, אפשר להמשיך לנתיב המבוקש
      return true;
    }),
    catchError(() => {
      // במקרה של שגיאה בשרת, נאפשר כניסה לדף הבית כברירת מחדל
      return of(true);
    })
  );
};