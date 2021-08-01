import React from 'react';
import Button from '@material-ui/core/Button';

interface ITopicButtonProps {
    onClick: (topicName: string) => void;
    topicName: string;
    isSelected: Boolean;
}

export default function TopicButton(props: ITopicButtonProps) {

    return (
        <Button variant="contained" onClick={props.onClick(props.topicName)} color={props.isSelected ? "primary" : ""}>
            {props.topicName}
        </Button>
    );
}