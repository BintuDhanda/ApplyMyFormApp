import React, {useCallback, useContext, useEffect, useState} from 'react';
import {
  StyleSheet,
  Text,
  Modal,
  TouchableOpacity,
  View,
  Pressable,
  FlatList,
  RefreshControl,
  Linking
} from 'react-native';
import {Button, TextInput, Title} from 'react-native-paper';
import * as DocumentPicker from 'react-native-document-picker';
import {Picker} from '@react-native-picker/picker';
import MaterialCommunityIcons from 'react-native-vector-icons/dist/MaterialCommunityIcons';
import Snackbar from 'react-native-snackbar';

import {AppContext} from '../../settings/Context';
import Colors from '../../settings/Colors';
import {CFetch, CFormFetch} from '../../settings/APIFetch';
import Loader from '../../utilities/Loader';
import DocumentItem from './DocumentItem';
var RNFS = require('react-native-fs');
import PushNotification from 'react-native-push-notification'
const Documents = () => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  const [editing, setEditing] = useState(false);
  const [visible, setVisible] = useState(false);
  const [data, setData] = useState([]);
  const take = 20;
  const [skip, setSkip] = useState(take);
  const qualifications = [
    {label: 'Aadhaar Card', value: 'Aadhaar Card'},
    {label: 'Pan Card', value: 'Pan Card'},
    {label: 'Voter Card', value: 'Voter Card'},
    {label: 'Driving License', value: 'Driving License'},
    {label: 'Income Certificate', value: 'Income Certificate'},
    {label: 'Cast Certificate', value: 'Cast Certificate'},
    {label: 'Photo', value: 'Upload Photo'},
    {label: 'Signature', value: 'Upload Signature'},
  ];
  const [model, setModel] = useState({
    id: '',
    name: '',
    file: '',
    fileUrl: '',
  });
  const handleInput = (name, value) => {
    setModel({
      ...model,
      [name]: value,
    });
  };
  const onSelectFile = name => {
    DocumentPicker.pick({}).then(res => {
      if (DocumentPicker.isCancel()) {
        console.log('Cancelled');
      } else {
        handleInput(name, {
          name: res[0].name,
          uri: res[0].uri,
          type: res[0].type,
        });
      }
    });
  };

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    setWaiting(true);
    setLoading(true);
    loadData();
  });

  useEffect(() => {
    setLoading(true);
    loadData();
  }, []);

  const loadData = isSkip => {
    CFetch('Documents', appUser.token, {
      skip: isSkip ? skip : 0,
      take: take,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            if (isSkip) {
              setSkip(skip + take);
              setData([...data, ...result]);
            } else {
              setSkip(take);
              setData(result);
            }
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setEditing(false);
        setLoading(false);
        setWaiting(false);
        setRefreshing(false);
      });
  };

  const handleSubmit = () => {
    var formData = new FormData();
    if (editing) {
      formData.append('Id', model.id);
    }
    formData.append('name', model.name);
    if (model.file) {
      formData.append('file', model.file);
    }

    let url = editing ? 'UpdateDocument' : 'CreateDocument';
    CFormFetch(url, appUser.token, formData)
      .then(res => {
        console.log(res.status);
        if (res.status === 200) {
          setVisible(false);
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
          });
        } else if (res.status === 441) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
          });
        } else {
          Snackbar.show({
            text: 'Something went wrong',
            duration: Snackbar.LENGTH_LONG,
          });
        }
      })
      .catch(error => {
        console.warn(error);
      })
      .finally(() => {
        loadData();
      });
  };
  const handleEdit = id => {
    setLoading(true);
    setWaiting(true);
    CFetch('GetDocumentById', appUser.token, {Id: id})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            setModel(result);
            setEditing(true);
            setVisible(true);
          });
        } else if (res.status === 441) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
          });
        } else {
          Snackbar.show({
            text: 'Something went wrong',
            duration: Snackbar.LENGTH_LONG,
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setWaiting(false);
        setLoading(false);
      });
  };
  const handleDelete = id => {
    setWaiting(true);
    setLoading(true);
    CFetch('DeleteDocument', appUser.token, {Id: id})
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            Snackbar.show({
              text: result,
              duration: Snackbar.LENGTH_LONG,
            });
          });
        } else if (res.status === 441) {
          Snackbar.show({
            text: result,
            duration: Snackbar.LENGTH_LONG,
          });
        } else {
          Snackbar.show({
            text: 'Something went wrong',
            duration: Snackbar.LENGTH_LONG,
          });
        }
      })
      .finally(() => {
        loadData();
      });
  };

  const handlDownload = async url => {
    let index = url.lastIndexOf('/');
    let file = url.substring(index);
    if (file.length > 1) {
      RNFS.downloadFile({
        fromUrl: url,
        toFile:
          RNFS.DownloadDirectoryPath + url.substring(url.lastIndexOf('/')),
      })
        .promise.then(r => {
          if (r.statusCode === 200) {
            PushNotification.createChannel(
              {
                channelId: "channel-id", // (required)
                channelName: "My channel", // (required)
              })
            PushNotification.localNotification({
              channelId:"channel-id",
              title:'Downloaded Successfully', 
              message:file,
              data:{
                url:RNFS.DownloadDirectoryPath + url.substring(url.lastIndexOf('/'))
              }
            })
            Snackbar.show({
              text: 'Downloaded Successfully',
              duration: Snackbar.LENGTH_LONG,
            });
          } else {
            Snackbar.show({
              text: 'Download failed try again',
              duration: Snackbar.LENGTH_LONG,
            });
          }
        })
        .catch(errr => {
          console.log(errr);
        });
    } else {
      Snackbar.show({
        text: 'No attachment found',
        duration: Snackbar.LENGTH_LONG,
      });
    }
  };

  return (
    <>
      <View style={styles.container}>
        <Loader loading={loading} waiting={waiting} spinner={true} />
        <View style={styles.btnrow}>
          <Button
            style={styles.addbtn}
            labelStyle={styles.btnLabel}
            onPress={() => setVisible(true)}>
            + Add Document
          </Button>
        </View>
        <FlatList
          data={data}
          refreshControl={
            <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
          }
          keyExtractor={(_, index) => index.toString()}
          onEndReached={() => loadData(true)}
          onEndReachedThreshold={0.2}
          renderItem={({item, index}) => (
            <DocumentItem
              item={item}
              index={index}
              handleEdit={handleEdit}
              handleDelete={handleDelete}
              handlDownload={handlDownload}
            />
          )}
        />
      </View>
      <Modal
        animationType="slide"
        visible={visible}
        onRequestClose={() => setVisible(false)}>
        <View style={styles.modalContainer}>
          <View style={styles.modalTitle}>
            <Title>{editing ? 'Update Document' : 'Add New Document'}</Title>
            <Pressable onPress={() => setVisible(false)}>
              <MaterialCommunityIcons name="close" size={25} />
            </Pressable>
          </View>
          <View style={[styles.pickerContainer, styles.inputContainer]}>
            <Picker
              style={styles.input}
              selectedValue={model.name}
              onValueChange={val => {
                handleInput('name', val);
              }}>
              <Picker.Item label="-- Select Document* --" value="" />
              {qualifications.map((item, index) => (
                <Picker.Item
                  key={index}
                  label={item.label}
                  value={item.value}
                />
              ))}
            </Picker>
          </View>
          <TouchableOpacity
            onPress={() => onSelectFile('file')}
            style={styles.inputContainer}>
            <TextInput
              value={model.file ? model.file.name : ''}
              theme={{colors: {primary: Colors.green}}}
              style={styles.input}
              placeholder="File"
              mode="outlined"
              outlineColor={Colors.primary}
              activeOutlineColor={Colors.primary}
              editable={false}
            />
            <Text style={styles.filePickerLabel}>Choose File</Text>
          </TouchableOpacity>
          <Button
            onPress={handleSubmit}
            style={styles.btn}
            labelStyle={styles.btnLabel}>
            {editing ? 'Update' : 'Save'}
          </Button>
        </View>
      </Modal>
    </>
  );
};

export default Documents;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  inputContainer: {
    marginBottom: 10,
  },
  input: {
    height: 45,
    marginBottom: 5,
  },
  pickerContainer: {
    paddingVertical: 2,
    borderRadius: 5,
    borderColor: Colors.transparentBlack,
    borderWidth: 1,
  },
  filePickerLabel: {
    position: 'absolute',
    color: Colors.white,
    right: 2,
    borderRadius: 3,
    top: 7,
    backgroundColor: '#4CA6A8',
    padding: 13,
  },
  btnrow: {
    alignItems: 'center',
  },
  addbtn: {
    marginTop: 15,
    backgroundColor: '#4CA6A8',
    color: 'white',
    width: '70%',
  },
  btn: {
    backgroundColor: '#4CA6A8',
    padding: 5,
  },
  btnLabel: {
    color: Colors.white,
  },
  modalContainer: {
    padding: 10,
  },
  modalTitle: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 5,
    justifyContent: 'space-between',
    marginBottom: 10,
  },
});
