import {StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {IconButton} from 'react-native-paper';
import FastImage from 'react-native-fast-image';

const QualificationItem = ({item, index, handleEdit, handleDelete}) => {
  return (
    <View key={index} style={styles.container}>
      <View style={styles.iconrow}>
        <IconButton
          icon="pencil"
          size={22}
          color="#61b6b8"
          onPress={() => handleEdit(item.id)}
        />
        <IconButton
          icon="delete"
          color="#ff4d4d"
          size={22}
          onPress={() => handleDelete(item.id)}
        />
      </View>
      <View style={styles.row}>
        <Text style={styles.title}>Qualification :</Text>
        <Text style={styles.text}>{item.name}</Text>
      </View>
      <View style={styles.row}>
        <FastImage
          source={{uri: item.fileUrl}}
          style={styles.doc}
          resizeMode={FastImage.resizeMode.center}
        />
      </View>
    </View>
  );
};

export default QualificationItem;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    marginTop: 10,
    padding: 20,
    paddingTop: 5,
  },
  row: {
    flexDirection: 'row',
    paddingTop: 5,
    paddingBottom: 5,
    paddingLeft: 8,
    textAlign: 'center',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#cccccc',
    borderRightWidth: 1,
    borderRightColor: '#cccccc',
    borderLeftWidth: 1,
    borderLeftColor: '#cccccc',
  },
  title: {
    fontSize: 16,
    fontWeight: '700',
    color: 'black',
  },
  text: {
    fontSize: 14,
    fontWeight: '500',
    paddingLeft: 10,
    paddingTop: 2,
  },
  iconrow: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    alignItems: 'flex-end',
  },
  doc: {
    width: '100%',
    height: 300,
  },
});
