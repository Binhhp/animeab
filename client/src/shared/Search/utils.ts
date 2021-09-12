
export function matchStateToTerm(state: any, value: string) {
    const regex = new RegExp(`${value.toLowerCase()}`);
    const title = state.title.toLowerCase();
    const titleSupport = state.titleVie.toLowerCase();
    return regex.test(title) || regex.test(titleSupport);
    // state.title.toLowerCase().indexOf(value.toLowerCase()) !== -1 
}
  
  
export function fakeRequest(value: any, list: any, cb: any) {

  try{
    return setTimeout(cb, 500, value ?
      list.filter((state: any) => matchStateToTerm(state, value)).slice(0, 6)
      : list
    )
  }
  catch{
    return list;
  }
}
  
  