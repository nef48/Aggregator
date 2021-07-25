import React from 'react';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import * as API from '../API/AggregatorAPI';

interface ISignupPageProps {
    isOpen: Boolean;
    onClose: () => void;
}

export default function SignupPage(props: ISignupPageProps) {
    const [username, setUsername] = React.useState("");
    const [password, setPassword] = React.useState("");
    const [confirmPassword, setConfirmPassword] = React.useState("");
    const [doPasswordsMatch, setDoPasswordsMatch] = React.useState(true);

    const handleUsernameChange = (event) => {
        setUsername(event.target.value);
    };

    const handlePasswordChange = (event) => {
        setPassword(event.target.value);
    };

    const handleConfirmPasswordChange = (event) => {
        setConfirmPassword(event.target.value);
    };

    const handleSubmitSignup = () => {
        if (password !== confirmPassword) {
            setDoPasswordsMatch(false);
        }
        else {
            setDoPasswordsMatch(true);

            
        }
    }

    return (
        <Dialog open={props.isOpen} onClose={props.onClose} aria-labelledby="form-dialog-title">
            <DialogTitle>Signup for News Aggregator </DialogTitle>
            <DialogContent>
                <React.Fragment>
                    <div>
                        <TextField
                            label="Username"
                            variant="outlined"
                            onChange={handleUsernameChange}
                            style={{ width: 400 }} />
                        <TextField
                            label="Password"
                            type="password"
                            variant="outlined"
                            onChange={handlePasswordChange}
                            style={{ width: 400, marginTop: 10 }} />
                        <TextField
                            error={!doPasswordsMatch}
                            label="Confirm Password"
                            type="password"
                            variant="outlined"
                            onChange={handleConfirmPasswordChange}
                            style={{ width: 400, marginTop: 10 }} />
                    </div>
                </React.Fragment>
            </DialogContent>
            <DialogActions>
                <Button color="primary" onClick={() => {handleSubmitSignup}}>
                    Signup
                </Button>
                <Button color="secondary" onClick={props.onClose}>
                    Cancel
                </Button>
            </DialogActions>
        </Dialog>
        
    );
}