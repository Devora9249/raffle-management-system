import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { catchError, map, of } from "rxjs";
import { RaffleService } from "../services/RaffleService";

export const raffleRedirectGuard: CanActivateFn = () => {
  const raffleService = inject(RaffleService);
  const router = inject(Router);

  return raffleService.getStatus().pipe(
    map((res) => {
      if (res.finished) {
        router.navigateByUrl("/winners");
        return false;
      }

      router.navigateByUrl("/");
      return false;
    }),
    catchError(() => {
      // אם יש תקלה בשרת—לא חוסמים את האתר
      router.navigateByUrl("/");
      return of(false);
    })
  );
};
