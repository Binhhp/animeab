export class Cookies {
  
    static getCookies(cname: string): any {
      let name = cname + "=";
      let ca = document.cookie.split(';');
      for(let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) === ' ') {
          c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
          return c.substring(name.length, c.length);
        }
      }
      return "";
    }

    static setCookies(cname: string, cvalue: any, exdays: number): void {
      const d = new Date();
      d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
      let expires = "expires="+d.toUTCString();
      document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    static checkCookies(cname: string): boolean {
      let user = Cookies.getCookies(cname);
      if (user !== "") {
        return true;
      } else {
        return false;
      }
    }
}