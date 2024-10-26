import React, {useState} from 'react';

import Header from './Header';

export default function App() {
  let counter=0

function increment(){
  counter ++
}

  return(
    <div>
    <Header>
      Counter: {counter}
    </Header>
    <button onClick={increment}>Add</button>
    </div>
    
  );
}
