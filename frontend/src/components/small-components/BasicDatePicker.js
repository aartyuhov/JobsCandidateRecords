import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DemoContainer} from "@mui/x-date-pickers/internals/demo";
import dayjs from "dayjs";

const BasicDatePicker = (
    props
) => {
    return (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DemoContainer components={['DatePicker']}>
                <DatePicker {...props} value={dayjs(props.value)} defaultValue={dayjs(props.defaultValue)}/>
            </DemoContainer>
        </LocalizationProvider>
    );
}
export  default BasicDatePicker;