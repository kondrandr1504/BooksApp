import * as React from 'react';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import { Stack, TextField } from '@mui/material';
import { CreateBook } from '../Client';
import { useState } from 'react';
import axios from 'axios';
import LoadingComponent from './LoadingComponent';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function AddBookField() {

  const [nameReq, setName] = useState<string>("");
  const [genreReq, setGenre] = useState<string>("");
  const [yearReq, setYear] = useState<number>(0);
  const [authorReq, setAuthor] = useState<string>("");
  const [isFull, setIsFull] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);

  //Проверка на заполненость формы
  React.useEffect(() => {
    if (nameReq !== "" && genreReq !== "" && yearReq !== 0 && authorReq !== "") {
      setIsFull(true);
    } else {
      setIsFull(false);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [nameReq, genreReq, yearReq, authorReq])

  var createRequestBody: CreateBook = {
    name: nameReq,
    genre: genreReq,
    year: yearReq,
    author: authorReq
  }

  const addBook = () => {
    axios.post("/books", createRequestBody)
      .then(resp => {
        setLoading(true);
        toast.success('Книга была успешно добавлена');
      })
      .catch(function (error) {
        console.log(error);
        toast.error('При добавлении книги произошла ошибка');
      });
  };

  React.useEffect(() => {
    setLoading(false);
  }, [loading]);

  return (
    <div>
      < Stack
        direction="column"
        justifyContent="center"
        alignItems="center"
        spacing={3}
      >
        <TextField id="name" label="Название" variant="filled"
          onInput={(e) => {
            setName((e.target as any).value);
          }}

          sx={{
            backgroundColor: "#98BF64",
            marginTop: "10px"
          }} />
        <TextField id="genre" label="Жанр" variant="filled"
          onInput={(e) => {
            setGenre((e.target as any).value);
          }}
          sx={{
            backgroundColor: "#98BF64",
          }} />
        <TextField id="author" label="Автор" variant="filled"
          onInput={(e) => {
            setAuthor((e.target as any).value);
          }}
          sx={{
            backgroundColor: "#98BF64"
          }} />
        <TextField id="year" label="Год" variant="filled"
          onInput={(e) => { setYear((e.target as any).value); }}
          sx={{ backgroundColor: "#98BF64" }} />
        {
          isFull ?
            <Button id="AddButton" startIcon={<AddIcon />} variant="contained"
              onClick={() => { addBook(); }}
              sx={{ backgroundColor: "green", color: "white", marginTop: "10px" }}>
              {('Добавить')}
            </Button> :
            <Button id="AddButton" startIcon={<AddIcon />} variant="contained" disabled
              onClick={() => { addBook(); }}
              sx={{ backgroundColor: "green", color: "white", marginTop: "10px" }}>
              {('Добавить')}
            </Button>
        }

      </Stack >
      {loading && <LoadingComponent />}
    </div>

  );
}