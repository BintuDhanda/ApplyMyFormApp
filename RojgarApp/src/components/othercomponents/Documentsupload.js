import React from "react";
import {View, Text, StyleSheet} from "react-native";

 const Documentsupload = ()=>
{
return(
    <View style={styles.container}>
        <Text>Document Upload Scren
        </Text>
    </View>
);
}

export default Documentsupload;

const styles = StyleSheet.create(
    {
        container:{
            justifyContent:"center",
            alignItems:"center",
            flex:1
        }
    }
);
