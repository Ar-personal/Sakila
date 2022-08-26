import React, {useState } from "react";
import './App.css';

function App(){ 

  let filmValues = {
    film: [],
    filmId: undefined,
    filmTitle: undefined,
    edit: undefined,
  };

  let updateFilmValues = {
    title: "",
    description: "",
    releaseYear: undefined,
    languageId: undefined,
    rentalDuration: undefined,
    rentalRate: undefined,
    length: undefined,
    replacementCost: undefined,
    rating: "",
  };

  const [filmData, setFilmData ] = useState(filmValues);
  const [updateFilm, setUpdateFilm ] = useState(updateFilmValues);
  const [actorId, setActorId ] = useState("");
  const [actor, setActor ] = useState([]);
  // const [film, setFilm] = useState([]);
  
  const [actorFilmIds, setActorFilmIds] = useState([]);
  const [actorFilmNames, setActorFilmNames] = useState([]);
  const [hidden, setHidden] = useState(false);
  const [deleteActorId, setDeleteActorId] = useState(undefined);
  const [isDeleted, setIsDeleted] = useState(undefined);

  // useEffect(() => {
  //   console.log({actorId});

  // }, [actorId, filmId])

  //HANDLERS
  const handleChangeActor = (event) =>{
    setActorId(event.target.value);
  };

  const handleChangeFilm = (e) =>{
    setFilmData({...filmData, [e.target.name]: e.target.value});
    console.log(filmData);
  };

  const handleEditFilm = (e) =>{
    e.preventDefault();
    console.log("handle change film: " + e.target.value);
    setUpdateFilm({...updateFilm, [e.target.name]: e.target.value})
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

    if(filmData.filmId !== undefined){
      console.log("find with id")
      FetchFilm('https://sakila20220809143255.azurewebsites.net/getFilmById/', filmData.filmId);
    }

    if(filmData.filmTitle !== undefined){
      console.log("find with title")
      FetchFilm('https://sakila20220809143255.azurewebsites.net/getFilmByTitle/', filmData.filmTitle);
    }
      DisplayFilm(filmData)
  };

  const handleSubmitEditFilm = (event) =>{
    console.log("handle submit edit film")
    event.preventDefault();
    FetchEditFilm();
  }

  const handleDeleteActor = (event) =>{
    console.log(deleteActorId, isDeleted)
    event.preventDefault();
    FetchDelete();
  }

  //API CALLS
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

function FetchEditFilm(){
  fetch(`https://sakila20220809143255.azurewebsites.net/updateFilm/${updateFilm.title}/${updateFilm.description}/${updateFilm.releaseYear}/${updateFilm.languageId}/${updateFilm.rentalDuration}/${updateFilm.rentalRate}/${updateFilm.length}/${updateFilm.replacementCost}/${updateFilm.rating}`, {
    method: "PUT"
   })
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
    })
  .catch((err) => {
      console.log(err.message);
  });
}

function FetchDelete(){
  fetch(`https://sakila20220809143255.azurewebsites.net/DeleteActorById/${deleteActorId}/${isDeleted}`, {
    method: "PUT"
   })
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
    })
  .catch((err) => {
      console.log(err.message);
  });
}

//MAIN RETURN
  return (


    <div className="main">
      <div class="wrapper">
      <h4 class = "col1">Search for an Actor</h4>
      <div class = "col1"> 
        <form>
            <input type="text" name="actorId" value={actorId} id="id" onChange={handleChangeActor} placeholder="Actor Id"></input>
            <input type="submit" onClick={handleSubmitActor}></input>
        </form>
      </div>
      <div class = "col2"> 
        {actor && <div class = "col col2-5"><DisplayActor props = {actor}/></div>}

        
      </div>

    <div class = "col5">
        <h4>Delete</h4>
        <form>
            <input type="text" name="deleteActorId" value={deleteActorId} onChange={(e) => setDeleteActorId(e.target.value)} placeholder="Actor Id"></input>
            <input type="text" name="isDeleted" value={isDeleted} onChange={(e) => setIsDeleted(e.target.value)} placeholder="true/false"></input>
            <input type="submit" onClick={handleDeleteActor}></input>
        </form>
      </div>

      <div class = "col1">
        <br />
        <h4>Search for a film</h4>
        <form>
            <input type="text" name="filmId" id="filmId" value={filmValues.filmId} onChange={handleChangeFilm} placeholder="Film Id"></input><br/>
            <input type="text" name="filmTitle" id="filmTitle" value={filmValues.filmTitle} onChange={handleChangeFilm} placeholder="Film Title"></input>
            <br/>
            <input name ="filmForm" type="submit" onClick={handleSubmitFilm}></input>
        </form>
      </div>


      {filmData.film && <div class="col col2-4"><DisplayFilm props = {filmData.film}/></div>}

      {hidden && <div class="col col4"><EditFilm props = {filmData.film}/></div>}
      </div>


    </div>
  );

//DISPLAY METHODS
  function DisplayFilmTitles(prop){
    console.log("film titles: ", prop)
    // setActorFilmIds(prop.filmActors);
    for(var i = 0; i < prop.filmActors.length; i++){
       fetch('https://sakila20220809143255.azurewebsites.net/getFilmById/' + prop.filmActors[i].filmId, {
         method: "GET"
       })
       .then((response) => response.json())
       .then((data) => {
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
              <div class="underlined" key = {index} onClick = {() => {TriggerFilmSearch(element)}}>{element}</div>
          );
        })}
      </div>
    ));
}

function TriggerFilmSearch(props){
  var t = props
  console.log("film search")
  document.getElementById('filmTitle').value = t;
  filmValues.film = t;
  console.log(t);
  FetchFilm('https://sakila20220809143255.azurewebsites.net/getFilmByTitle/', t);
  DisplayFilm(filmValues.film);
}


function DisplayFilm({props}){
  console.log(props);
  // var arr = props.inventories;
    return props?.map(prop => (
      <div class = "col2" key={"filmDiv"}>
        <p key={"title"}>Title: {prop.title}</p>
        <p key={"desc"}>Description: {prop.description}</p>
        <p key={"rel"}>Release Year: {prop.releaseYear}</p>
        <p key={"lang"}>Language Id: {prop.languageId}</p>
        <p key={"rent"}>Rental Duration: {prop.rentalDuration}</p>
        <p key={"rate"}>Rental Rate: {prop.rentalRate}</p>
        <p key={"len"}>Length: {prop.length}</p>
        <p key={"repl"}>Replacement Cost: {prop.replacementCost}</p>
        <p key={"rating"}>Rating: {prop.rating}</p>
        {/* <p>Inventories: {arr[0]}</p> */}
        
        <button onClick={() => {setHidden(!hidden)}}>Edit Film</button>
      </div>

    ));
}

function EditFilm({props}){
  return props?.map(prop => (
    updateFilmValues.title = prop.title,
    updateFilmValues.description = prop.description,
    updateFilmValues.releaseYear = prop.releaseYear,
    updateFilmValues.languageId = prop.languageId,
    updateFilmValues.rentalDuration = prop.rentalDuration,
    updateFilmValues.length = prop.length,
    updateFilmValues.rentalRate = prop.rentalRate,
    updateFilmValues.replacementCost = prop.replacementCost,
    updateFilmValues.rating = prop.rating,

    <div key={"edit film div"}>
    <h4 key={"edit film h4"}>Edit Film</h4>
    <form class = "col5">
          <input type="text" name="title" value={updateFilm.title} placeholder = {prop.title} onChange = {handleEditFilm} key={prop.title}></input>
          <input type="text" name="description" value={updateFilm.description} placeholder = {"Desc: " + prop.description} onChange = {handleEditFilm} key={prop.description}></input>
          <input type="text" name="releaseYear" value={updateFilm.releaseYear} placeholder = {"Release: " + prop.releaseYear} onChange = {handleEditFilm} key={prop.releaseYear}></input>
          <input type="text" name="languageId" value={updateFilm.languageId} placeholder = {"Lang id: " + prop.languageId} onChange = {handleEditFilm} key={prop.languageId}></input>
          <input type="text" name="rentalDuration" value={updateFilm.rentalDuration} placeholder = {"Rental duration: " + prop.rentalDuration} onChange = {handleEditFilm} key={prop.rentalDuration}></input>
          <input type="text" name="rentalRate" value={updateFilm.rentalRate} placeholder = {"Rate: " + prop.rentalRate} onChange = {handleEditFilm} key={prop.rentalRate}></input>
          <input type="text" name="length" value={updateFilm.length} placeholder = {"Length: " + prop.length} onChange = {handleEditFilm} key={prop.length}></input>
          <input type="text" name="replacementCost" value={updateFilm.replacementCost} placeholder = {"Repl. cost: " + prop.replacementCost} onChange = {handleEditFilm} key={prop.replacementCost}></input>
          <input type="text" name="rating" value={updateFilm.rating} placeholder = {"Rating: " + prop.rating} onChange = {handleEditFilm} key={prop.rating}></input>
          {/* <p>Inventories: {arr[0]}</p> */}
          <br/>
          <input type="submit" onClick={handleSubmitEditFilm}></input>
        </form>
        </div>
    ));
  }





}





export default App;
