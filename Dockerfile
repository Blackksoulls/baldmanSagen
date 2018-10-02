FROM microsoft/dotnet:2.1-runtime-alpine

WORKDIR /root/

ADD . ./baldmanSagen

WORKDIR /root/baldmanSagen/Publish/

CMD ["sh","./run.sh"]