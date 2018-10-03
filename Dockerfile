FROM microsoft/dotnet:2.1-runtime-alpine

EXPOSE 666

WORKDIR /root/

ADD . ./baldmanSagen

WORKDIR /root/baldmanSagen/Publish/

CMD ["sh","./run.sh"]