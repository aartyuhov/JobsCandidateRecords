import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DemoContainer} from "@mui/x-date-pickers/internals/demo";

const BasicDatePicker = (
    props
) => {
    return (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DemoContainer components={['DatePicker']}>
                <DatePicker label={props.label}
                            name={props.name}
                            className={props.className}
                            required/>
            </DemoContainer>
        </LocalizationProvider>
    );
}
export  default BasicDatePicker;