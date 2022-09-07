import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Books, BooksClient } from '../Client';
import { useState } from 'react';

const BooksTable: React.FC = () => {

  // const [booksData, setBooksData] = useState<Books[]>([]);
  // eslint-disable-next-line react-hooks/exhaustive-deps
  const booksClient = new BooksClient();

  var booksData = booksClient.GetBooks();

  return (
    <TableContainer component={Paper} sx={{ backgroundColor: "#bfc1c2" }}>
      <Table sx={{ minWidth: 400 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>id</TableCell>
            <TableCell align="right">Название</TableCell>
            <TableCell align="right">Автор</TableCell>
            <TableCell align="right">Жанр</TableCell>
            <TableCell align="right">Год</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {booksData.map((row) => (
            <TableRow
              key={row.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell align="right">{row.id}</TableCell>
              <TableCell align="right">{row.name}</TableCell>
              <TableCell align="right">{row.author}</TableCell>
              <TableCell align="right">{row.genre}</TableCell>
              <TableCell align="right">{row.year}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
export default BooksTable;

