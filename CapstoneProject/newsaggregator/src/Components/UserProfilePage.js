import React from 'react';
import UserData from '../Classes/UserData';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';

interface IUserProfilePageProps {
    isOpen: Boolean;
    onClose: () => void;
    changeTopics: () => void;
    logOff: () => void;
    user: UserData;
}

export default function UserProfilePage(props: IUserProfilePageProps) {

    const handleLogOff = () => {
        props.logOff();
    }

    const handleChangeTopics = () => {
        props.changeTopics();
    }

    return (
        <Dialog open={props.isOpen} onClose={props.onClose} aria-labelledby="form-dialog-title">
            <DialogTitle>{props.user.username}</DialogTitle>
            <DialogContent>
                <React.Fragment>
                    <div>
                        
                    </div>
                </React.Fragment>
            </DialogContent>
            <DialogActions>
                <Button color="primary" onClick={handleChangeTopics}>
                    Update Topics
                </Button>
                <Button color="secondary" onClick={handleLogOff}>
                    Log Off
                </Button>
                <Button color="secondary" onClick={props.onClose}>
                    Close
                </Button>
            </DialogActions>
        </Dialog>
    )
}