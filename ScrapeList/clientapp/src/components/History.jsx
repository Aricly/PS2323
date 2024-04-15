import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

function createData(materialName, priceChange) {
    return { materialName, priceChange };
}

const rows = [
    createData('Aggregate Blue Metal 20mm', '+5%'),
    // Add more rows as
];

export default function MaterialTable() {
    return (
        <TableContainer component={Paper} sx={{ maxHeight: 400, overflow: 'auto' }}>

            <Table aria-label="material table">
                <TableHead>
                    <TableRow>
                        <TableCell>Material name</TableCell>
                        <TableCell>Price change</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.materialName}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.materialName}
                            </TableCell>
                            <TableCell>{row.priceChange}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}





