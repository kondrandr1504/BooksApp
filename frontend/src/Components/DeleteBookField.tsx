import * as React from 'react';
import Button from '@mui/material/Button';
import { Stack, TextField } from '@mui/material';
import { useState } from 'react';
import axios from 'axios';
import LoadingComponent from './LoadingComponent';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import DeleteIcon from '@mui/icons-material/Delete';

export default function DeleteBookField() {

  const [nameReq, setName] = useState<string>("");
  const [isFull, setIsFull] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);

  //Проверка на заполненость формы
  React.useEffect(() => {
    if (nameReq !== "") {
      setIsFull(true);
    } else {
      setIsFull(false);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [nameReq])

  const addBook = () => {
    var url = "/books/" + encodeURIComponent(nameReq);
    axios.delete(url)
      .then(resp => {
        setLoading(true);
        toast.success('Книга была успешно удалена');
      })
      .catch(function (error) {
        console.log(error);
        toast.error('При удалении книги произошла ошибка');
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
        {
          isFull ?
            <Button id="AddButton" startIcon={<DeleteIcon />} variant="contained"
              onClick={() => { addBook(); }}
              sx={{ backgroundColor: "green", color: "white", marginTop: "10px" }}>
              {('Добавить')}
            </Button> :
            <Button id="AddButton" startIcon={<DeleteIcon />} variant="contained" disabled
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