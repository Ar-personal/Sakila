import React, { startTransition, useEffect, useState } from "react";
import { Component } from "react";
import './Home.css';

function Home(){ 

  let filmValues = {
    film: [],
    filmId: undefined,
    filmTitle: undefined,
  };

  const [actorId, setActorId ] = useState(null);
  const [actor, setActor ] = useState([]);
  // const [film, setFilm] = useState([]);
  const [filmData, setFilmData ] = useState(filmValues);
  const [actorFilmIds, setActorFilmIds] = useState([]);
  const [actorFilmNames, setActorFilmNames] = useState([]);
  // const [filmTitle, setFilmTitle] = useState(undefined);

  // useEffect(() => {
  //   console.log({actorId});

  // }, [actorId, filmId])


  const handleChangeActor = (event) =>{
    setActorId(event.target.value);
  };

  const handleChangeFilm = (e) =>{
    setFilmData({...filmData, [e.target.name]: e.target.value});
    console.log(filmData);
  };

  const handleSubmitActor = (event) =>{
    event.preventDefault();
      FetchActorViaId();
      DisplayActor(filmData.film);
  };

  const handleSubmitFilm = (event) =>{
    if(event !== undefined){
      event.preventDefault();
    }
    
    console.log("form submitted")
      FetchFilm('https://sakila20220809143255.azurewebsites.net/getFilmById/', filmData.filmId);
      DisplayFilm(filmData)
  };



  function FetchActorViaId(){
    fetch(`https://sakila20220809143255.azurewebsites.net/getActorById/ ${actorId}`, {
      method: "GET"
    })
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
      setActor(data);
      console.log({data});
  })
  .catch((err) => {
      console.log(err.message);
  });
}

function FetchFilm(fetchString, fetchVia){
    //unsure of when to use variables, or to structure fall-back scenarios should fix at some point, id may be stale so reset sometime?
    // if(filmData.filmTitle !== undefined){
    //   fetchString = `https://sakila20220809143255.azurewebsites.net/getFilmByTitle/ ${filmData.filmTitle}`;
    // }else{
    //   var fetchString = `https://sakila20220809143255.azurewebsites.net/getFilmById/ ${filmData.filmId}`;
    // }
    fetch(fetchString + fetchVia, {
      method: "GET"
    })
    .then((response) => response.json())
    .then((data) => {
      console.log("fetched film data: ", data)
      setFilmData({film: data});
  })
  .catch((err) => {
      console.log(err.message);
  });
}

  return (

    <div className="home">
      <div class="container">
    <form class = "col-lg-4">
        <label for="id">Actor Id:</label>
        <input type="text" name="actorId" value={actorId} id="id" onChange={handleChangeActor}></input>
        <input type="submit" onClick={handleSubmitActor}></input>
    </form>

    {actor && <div><DisplayActor props = {actor}/></div>}

    <form class = "col-lg-4">
        <label for="id">Film Id:</label>
        <input type="text" name="filmId" id="filmId" value={filmValues.filmId} onChange={handleChangeFilm}></input>
        <label for="id">Film Title:</label>
        <input type="text" name="filmTitle" id="filmTitle" value={filmValues.filmTitle} onChange={handleChangeFilm}></input>
        <input name ="filmForm" type="submit" onClick={handleSubmitFilm}></input>
    </form>

    {filmData.film && <div><DisplayFilm props = {filmData.film}/></div>}

      </div>
    </div>
  );


  function DisplayFilmTitles(prop){
    console.log("film titles: ", prop)
    // setActorFilmIds(prop.filmActors);
    for(var i = 0; i < prop.filmActors.length; i++){
       fetch('https://sakila20220809143255.azurewebsites.net/getFilmById/' + prop.filmActors[i].filmId, {
         method: "GET"
       })
       .then((response) => response.json())
       .then((data) => {
        console.log("fetching film title " , data)
         data?.map((prop) => (
            setActorFilmNames(actorFilmNames => [...actorFilmNames, prop.title])
         ));
     })
     .catch((err) => {
         console.log(err.message);
     });
    }

  }
  
  function DisplayActor({props}){
    return props?.map((prop) => (
      <div>
        <p>First name: {prop.firstName}</p>
        <p>Last name: {prop.lastName}</p>
        <div>
          <button onClick = {function(event){DisplayFilmTitles(prop);}}>
            Show Films
          </button>
        </div>
        <br/>
        {actorFilmNames.map((element, index) =>{
          return (
              <button key = {index} onClick = {() => {TriggerFilmSearch(element)}}>{element}</button>
          );
        })}
      </div>
    ));
}

function TriggerFilmSearch(props){
  var t = props
  console.log("film search")
  document.getElementById('filmTitle').value = t;
  console.log(t);
  FetchFilm('https://sakila20220809143255.azurewebsites.net/getFilmByTitle/', t);
  DisplayFilm(filmValues.film);
}

}



function DisplayFilm({props}){
  console.log(props);
  // var arr = props.inventories;
    return props.map(prop => (
      <div>
        <p>Title: {prop.title}</p>
        <p>Description: {prop.description}</p>
        <p>Release Year: {prop.releaseYear}</p>
        <p>Language ID: {prop.languageId}</p>
        <p>Rental Duration: {prop.rentalDuration}</p>
        <p>Rental Rate: {prop.rentalRate}</p>
        <p>Length: {prop.length}</p>
        <p>Replacement Cost: {prop.replacementCost}</p>
        <p>Rating: {prop.rating}</p>
        {/* <p>Inventories: {arr[0]}</p> */}
      </div>
    ));
}


export default Home;