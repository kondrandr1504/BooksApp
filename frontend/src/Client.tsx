import axios from 'axios'
import { useEffect, useState } from 'react';

export interface Books {
  id: number,
  name: string,
  genre: string,
  author: string,
  year: number,
}

export interface CreateBook {
  name: string | undefined,
  genre: string | undefined,
  author: string | undefined,
  year: number | undefined,
}

export class BooksClient {
  /**
       *GET /books Получение списка книг
       */
  GetBooks = () => {

    const [bookState, setBookState] = useState<Books[]>([]);

    useEffect(() => {
      const config = {
        headers: {
          "Access-Control-Allow-Origin": "*",
        }
      };
      const apiUrl = '/books';
      axios.get(apiUrl, config).then((resp) => {
        var books = resp.data.items;

        setBookState(books);
      });
    }, [setBookState]);

    return bookState
  }

  /**
         *POST /books Добавление книги
         */
  PostBooks = (body: CreateBook) => {
    let loading = false

    const config = {
      headers: {
        "Content-Type": "application/json",
      }
    };
    const apiUrl = '/books';
    axios.post(apiUrl, body, config)
      .then(resp => {
        loading = true;
      })
      .catch(function (error) {
        console.log(error);
      });
    debugger
    return loading;
  }
}