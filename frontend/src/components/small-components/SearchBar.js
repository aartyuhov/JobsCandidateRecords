import React from 'react';
import { TextField, InputAdornment } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';

const SearchBar = ({ search, handleSearchChange }) => {
    return (
        <TextField
            label="Search"
            variant="outlined"
            value={search}
            onChange={handleSearchChange}
            className="search-bar"
            InputProps={{
                startAdornment: (
                    <InputAdornment position="start">
                        <SearchIcon />
                    </InputAdornment>
                ),
            }}
            sx={{
                width: '100%',
                maxWidth: '500px', // Ограничение ширины для современного вида
                '& .MuiOutlinedInput-root': {
                    borderRadius: '8px',
                },
                '& .MuiOutlinedInput-input': {
                    padding: '12px',
                },
                '& .MuiInputLabel-root': {
                    color: '#888', // Цвет текста лейбла
                },
                '& .MuiInputLabel-root.Mui-focused': {
                    color: '#3f51b5', // Цвет текста лейбла при фокусе
                },
                '& .MuiOutlinedInput-notchedOutline': {
                    borderColor: '#ddd', // Цвет границы
                },
                '& .MuiOutlinedInput-root.Mui-focused .MuiOutlinedInput-notchedOutline': {
                    borderColor: '#3f51b5', // Цвет границы при фокусе
                },
            }}
        />
    );
};

export default SearchBar;
